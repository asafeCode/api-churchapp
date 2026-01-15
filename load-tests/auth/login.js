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
        timeout: '60s', // evita abortos prematuros
    };

    const res = http.post('http://localhost:7144/auth/login', payload, params);

    let body;
    try {
        body = JSON.parse(res.body);
    } catch (err) {
        console.error('Erro ao parsear JSON do login:', err, 'Body recebido:', res.body);
        return { accessToken: null, refreshToken: null };
    }

    // Validar antes de retornar
    if (!body?.tokens?.accessToken) {
        console.error('AccessToken não encontrado no body:', body);
        return { accessToken: null, refreshToken: null };
    }

    check(res, {
        'login status 200': (r) => r.status === 200,
        'access token exists': (r) => body.tokens.accessToken !== undefined
    });

    return {
        accessToken: body.tokens.accessToken,
        refreshToken: body.tokens.refreshToken,
    };
}
