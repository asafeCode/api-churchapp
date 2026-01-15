import http from 'k6/http';
import { check, sleep } from 'k6';
import {login} from '../auth/login.js';
import { randomIntBetween, randomItem }
    from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';


export const options = {
    stages: [
        { duration: '30s', target: 10 },
        { duration: '30s', target: 25 },
        { duration: '30s', target: 15 },
    ],
};

export default function () {
    login()
    sleep(1);
}
