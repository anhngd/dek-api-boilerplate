import http from 'k6/http';
import { sleep } from 'k6';

const apiEndpoint = 'http://localhost:9101';


export default function () {
    const url = apiEndpoint + '/api/user/signup';
    const payload = JSON.stringify({
        "username": "string",
        "password": "string",
        "email": "string"
    });
    http.post(url, payload);
}