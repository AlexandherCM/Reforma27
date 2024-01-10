class ApiClient {
    //static host = 'https://reforma27.cohabi.com.mx/';
    ////static host = 'https://webapitest.bsite.net/';
    //static host = 'https://localhost:7223/';

    // Utiliza este metodo para mandar parametros a un controlador y recibir un JSON 
    async get(endpoint) {
        const response = await fetch(`${ApiClient.host}${endpoint}`);

        // (código de estado 200)
        if (!response.ok) {
            throw new Error(`Error al obtener datos. Código de estado: ${response.status}`);
        }
        return response;
    }

    async SendGet(endpoint) {
        const response = await fetch(`${ApiClient.host}${endpoint}`);
        return await response.json();
    }

    //Utiliza este metodo solo para pasar objetos a un controlador
    async SetPost(endpoint, data) {
        const response = await fetch(`${ApiClient.host}${endpoint}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });
    }

    //Utiliza este metodo para pasar objetos a un controlador y recibir un JSON
    async SendPost(endpoint, data) {
        const response = await fetch(`${ApiClient.host}${endpoint}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });
        return await response.json();
    }
}