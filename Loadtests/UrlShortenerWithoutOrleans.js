import http from 'k6/http';

export default function () {
    http.get('http://localhost:5062/shorten?url=http://google.nl');
}