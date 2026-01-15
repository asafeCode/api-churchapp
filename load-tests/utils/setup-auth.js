import http from 'k6/http';
import { check } from 'k6';

export function login() {
    const payload = JSON.stringify({
        name: "admin",
        password: "admin123",
        tenantId: "2be5a47c-65c6-45d7-a10c-747207b2e169"
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const res = http.post(
        'https://localhost:7144/auth/login',
        payload,
        params
    );

    check(res, {
        'login status 200': (r) => r.status === 200,
        'access token exists': (r) =>
            JSON.parse(r.body)?.tokens?.accessToken !== undefined
    });

    const body = JSON.parse(res.body);

    return body.tokens.accessToken;
}
