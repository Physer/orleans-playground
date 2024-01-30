import http from 'k6/http';

export default function () {
    http.get('http://localhost:5146/shorten?url=http://google.nl');
}