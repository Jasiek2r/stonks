const express = require('express');
const app = express();
const PORT = 5000;

app.get('/api/hello', (req, res) => {
    res.json({ message: 'Czeœæ z backendu!' });
});

app.listen(PORT, () => {
    console.log(`Serwer dzia³a na http://localhost:${PORT}`);
});
