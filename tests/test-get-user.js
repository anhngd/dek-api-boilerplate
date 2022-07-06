import http from 'k6/http';
import { sleep } from 'k6';

const apiEndpoint = 'http://localhost:9101';

export default function () {
    const url = apiEndpoint + '/api/user/08da5e40-4b00-45e7-84a5-4285b94db2a5';
    http.get(url);
}