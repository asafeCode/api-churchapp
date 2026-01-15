import http from 'k6/http';
import { check, sleep } from 'k6';
import { SharedArray } from 'k6/data';
import {login} from '../auth/login.js';

const filters = new SharedArray('filters', function() {
    return [
        { DateFrom: '2025-08-01', DateTo: '2025-08-31' },
        { DateFrom: '2024-01-01', DateTo: '2026-01-01' },
        { DateFrom: '2025-03-01', DateTo: '2025-03-31' },
    ];
});
export const options = {
    scenarios: {
        stress_test: {
            executor: 'ramping-vus',
            startVUs: 10,
            stages: [
                { duration: '30s', target: 20 },
                { duration: '30s', target: 40 },
                { duration: '30s', target: 60 },
                { duration: '30s', target: 80 },
                { duration: '30s', target: 100 },
            ],
            gracefulRampDown: '30s',
            gracefulStop: '30s',
        },
        endurance_test: {
            executor: 'constant-vus',
            vus: 50,
            duration: '5m', // mantém carga constante para testar endurance
        },
    },
    thresholds: {
        http_req_failed: ['rate<0.01'],
        http_req_duration: ['p(95)<1000'], // query pesada pode aceitar até 1s
    },
};

const BASE_URL = 'http://localhost:7144';
export function setup() {
    const accessToken = login();

    return {
        accessToken
    };
}
export default function (data) {
    const headers = {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${data.accessToken}`
        }
    };
    // Seleciona filtro aleatório para simular diferentes datas
    const filter = filters[Math.floor(Math.random() * filters.length)];
    const url = `${BASE_URL}/report/monthly-summary?DateFrom=${filter.DateFrom}&DateTo=${filter.DateTo}`;

    const res = http.get(url, headers);

    check(res, {
        'status 200': (r) => r.status === 200,
        'tem dados': (r) => r.json() !== undefined && r.json().period !== undefined, // ajuste conforme seu ResponseMonthlySummaryReadModel
    });

    sleep(1); // pausa entre requisições
}
