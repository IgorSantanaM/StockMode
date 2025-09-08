import React, { useState, useEffect } from 'react';
import { useAuth } from 'react-oidc-context';
import api from '../../services/api';

const AuthTest = () => {
  const auth = useAuth();
  const [userInfo, setUserInfo] = useState(null);
  const [apiTest, setApiTest] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const testApi = async () => {
    setLoading(true);
    setError(null);
    try {
      // Test public endpoint
      const publicResponse = await api.get('/user/test');
      setApiTest(publicResponse.data);

      // Test authenticated endpoint
      const userResponse = await api.get('/user/info');
      setUserInfo(userResponse.data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (auth.user) {
      testApi();
    }
  }, [auth.user]);

  if (!auth.isAuthenticated) {
    return <div>Please login to view this page.</div>;
  }

  return (
    <div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto' }}>
      <h1>Authentication Test</h1>
      
      <div style={{ marginBottom: '20px' }}>
        <h2>Authentication Status</h2>
        <p><strong>Authenticated:</strong> {auth.isAuthenticated ? 'Yes' : 'No'}</p>
        <p><strong>Loading:</strong> {auth.isLoading ? 'Yes' : 'No'}</p>
        {auth.user && (
          <div>
            <p><strong>User ID:</strong> {auth.user.profile?.sub}</p>
            <p><strong>Name:</strong> {auth.user.profile?.name}</p>
            <p><strong>Email:</strong> {auth.user.profile?.email}</p>
            <p><strong>Access Token:</strong> {auth.user.access_token ? 'Present' : 'Missing'}</p>
          </div>
        )}
      </div>

      <div style={{ marginBottom: '20px' }}>
        <h2>API Test</h2>
        <button onClick={testApi} disabled={loading}>
          {loading ? 'Testing...' : 'Test API Endpoints'}
        </button>
        
        {error && (
          <div style={{ color: 'red', marginTop: '10px' }}>
            <strong>Error:</strong> {error}
          </div>
        )}
        
        {apiTest && (
          <div style={{ marginTop: '10px' }}>
            <h3>Public Endpoint Response:</h3>
            <pre style={{ background: '#f5f5f5', padding: '10px', borderRadius: '4px' }}>
              {JSON.stringify(apiTest, null, 2)}
            </pre>
          </div>
        )}
        
        {userInfo && (
          <div style={{ marginTop: '10px' }}>
            <h3>Protected Endpoint Response:</h3>
            <pre style={{ background: '#f5f5f5', padding: '10px', borderRadius: '4px' }}>
              {JSON.stringify(userInfo, null, 2)}
            </pre>
          </div>
        )}
      </div>

      <div>
        <h2>Raw Token</h2>
        {auth.user?.access_token && (
          <textarea 
            style={{ width: '100%', height: '100px', fontFamily: 'monospace', fontSize: '12px' }}
            value={auth.user.access_token}
            readOnly
          />
        )}
      </div>
    </div>
  );
};

export default AuthTest;