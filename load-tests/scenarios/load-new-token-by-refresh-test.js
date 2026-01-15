import http from 'k6/http';
import { check, sleep } from 'k6';
import { SharedArray } from 'k6/data';
import { login } from '../auth/login.js';

const BASE_URL = 'http://localhost:7144';

// Configuração do teste
export const options = {
    stages: [
        { duration: '30s', target: 2 },    // sobe para 2 VUs
        { duration: '1m', target: 5 },     // sobe para 5 VUs
        { duration: '2m', target: 5 },     // mantém 5 VUs
    ],
    thresholds: {
        http_req_failed: ['rate<0.01'],  // <1% falhas
        http_req_duration: ['p(95)<500'], // 95% das requisições < 500ms
    },
};

// Executa login uma vez e retorna os tokens para todos os VUs
export function setup() {
    const data = login();
    console.log('Token inicial:', data.refreshToken);
    return data;
}

// Função executada por cada VU infinitamente
export default function (data) {
    let refreshToken = data.refreshToken;

    // Loop infinito seguro
    while (true) {
        if (!refreshToken) {
            console.error('RefreshToken indefinido! Abortando VU.');
            return;
        }

        const payload = JSON.stringify({ refreshToken });

        const res = http.post(`${BASE_URL}/token/refresh`, payload, {
            headers: { 'Content-Type': 'application/json' },
        });

        if (res.status !== 200) {
            console.error(`Erro na requisição: status=${res.status}`, res.body);
            return;
        }

        let resBody;
        try {
            resBody = JSON.parse(res.body);
        } catch (e) {
            console.error('Resposta inválida, não é JSON:', res.body);
            return;
        }

        // Checagens
        check(res, {
            'status 200': (r) => r.status === 200,
            'novo refreshToken existe': () => resBody.refreshToken !== undefined,
        });

        // Atualiza token para próxima iteração
        refreshToken = resBody.refreshToken;

        // Pausa de 1 segundo entre requisições
        sleep(1);
    }
}
