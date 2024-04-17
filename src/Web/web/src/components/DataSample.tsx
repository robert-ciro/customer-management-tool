import { useState } from 'react';
import axios from 'axios';

function DataSample() {
    const [dataCreated, setDataCreated] = useState<boolean>(false);
    const [dataRemoved, setDataRemoved] = useState<boolean>(false);
    const [showLoading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string>('');

    const handleCreateData = async () => {
        try {
            setLoading(true);
            setDataRemoved(false);
            await axios.post('/sample-data');
            setDataCreated(true);
            setError('');
        } catch (error: unknown) {
            // Handle error response
            setDataCreated(false);

            setError('Failed to create data: ' + error.message);
        } finally {
            setLoading(false);
        }
    };

    const handleRemoveData = async () => {
        try {
            setLoading(true);
            setDataRemoved(false);

            await axios.delete('/sample-data');
            setDataRemoved(true);
            setError('');
        } catch (error: unknown) {
            setDataRemoved(false);
            setError('Failed to delete data: ' + error.message);
        } finally {
            setLoading(false);
        }
    };

  return (
      <div>
          <button onClick={handleCreateData}>Create Data Sample</button>
          <button onClick={handleRemoveData}>Remove All Data</button>
          {dataCreated && !dataRemoved && <p>Data has been created!</p>}
          {dataRemoved && <p>Data has been removed!</p>}
          {error && <p>{error}</p>}
          {showLoading && <p>Waiting a response from the server...</p>}
      </div>
  );
}

export default DataSample;