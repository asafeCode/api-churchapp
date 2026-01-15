import http from 'k6/http';
import {check, sleep} from 'k6';

import {login} from '../auth/login.js';
import {InflowBuilder} from '../builders/inflow-builder.js';

export const options = {
    stages: [
        {duration: '30s', target: 10},  // 20 VUs nos primeiros 30s
        {duration: '30s', target: 30},  // sobe para 40 VUs nos próximos 30s
        {duration: '30s', target: 50},  // sobe para 60 VUs nos últimos 30s
    ],
    thresholds: {
        http_req_failed: ['rate<0.01'],
        http_req_duration: ['p(95)<800'],
    },
};

const BASE_URL = 'http://localhost:7144';
const INFLOW_URL = `${BASE_URL}/inflow`;

const USER_ID = '1dd84f4e-51aa-46ee-8a86-57bd5e8716b7';
const WORSHIP_ID = '7f13e11a-ab6c-4f32-891a-b03a449d33d5';

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

    const inflow = InflowBuilder()
        .withDate(new Date(2025, 0, 1), new Date())
        .withType()
        .withPaymentMethod()
        .withAmount(50, 500)
        .withDescription()
        .withUser(USER_ID)
        .withWorship(WORSHIP_ID)
        .build();

    const res = http.post(
        INFLOW_URL,
        JSON.stringify(inflow),
        headers
    );

    check(res, {
        'status is 201 or 200': (r) => r.status === 201 || r.status === 200,
    });

    sleep(0.5); // controla taxa de escrita
}
