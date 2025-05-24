import React, { useState } from 'react';
import Register from '../components/Register';

function App() {
  const [showAuth, setShowAuth] = useState(false);
  const [showRegister, setShowRegister] = useState(false);

  return (
    <div>
      <button>Wykresy</button>
      <button onClick={() => setShowAuth(true)}>Logowanie/Rejestracja</button>
      {showAuth && (
        <div style={{
          position: 'fixed', top: 0, left: 0, width: '100vw', height: '100vh',
          background: 'rgba(0,0,0,0.5)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 1000
        }}>
          <div style={{ background: '#fff', padding: 20, borderRadius: 8, minWidth: 300 }}>
            <button style={{ float: 'right' }} onClick={() => setShowAuth(false)}>Zamknij</button>
            {!showRegister ? (
              <div>
                <h2>Logowanie</h2>
                <input type="text" placeholder="Login" style={{ display: 'block', marginBottom: 10, width: '100%' }} />
                <input type="password" placeholder="Hasło" style={{ display: 'block', marginBottom: 10, width: '100%' }} />
                <button style={{ marginRight: 10 }}>Zaloguj</button>
                <button onClick={() => setShowRegister(true)}>Przejdź do rejestracji</button>
              </div>
            ) : (
              <div>
                <Register />
                <button onClick={() => setShowRegister(false)} style={{ marginTop: 10 }}>Powrót do logowania</button>
              </div>
            )}
          </div>
        </div>
      )}
    </div>
  );
}

export default App;