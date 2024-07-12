import React, { useState, useEffect } from 'react';
import axios from 'axios';

export function ApiComponent(){
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    // Define the API call in an async function
    const fetchData = async () => {
      try {
        const response = await axios.get('https://localhost:7165/v1/checkout/stock', 
            {
                headers: {
                    'x-api-key': '0543ef62c7df4b8c97fab1cab476491c'
                }                
            }
        );
        setData(response.data);
        setLoading(false);
      } catch (err) {
        setError(err);
        setLoading(false);
      }
    };

    fetchData();
  }, []); // Empty dependency array means this effect runs once on mount

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error.message}</div>;

  return (
    <div>
      <h1>API Data</h1>
      <ul>
        {data.map((item) => (
          <li key={item.id}>{item.id}: £{item.price}</li>
        ))}
      </ul>
    </div>
  );
};