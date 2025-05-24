const express = require('express');
const app = express();
const PORT = 5000;

app.get('/api/hello', (req, res) => {
    res.json({ message: 'Cze�� z backendu!' });
});

app.listen(PORT, () => {
    console.log(`Serwer dzia�a na http://localhost:${PORT}`);
});
