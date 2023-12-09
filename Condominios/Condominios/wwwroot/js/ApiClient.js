class ApiClient {

    async get(endpoint) {
        const response = await fetch(endpoint);
        return await response.json();
    }
        
    async SetPost(endpoint, data) {
        const response = await fetch(endpoint, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });
    }

    async SendPost(endpoint, data) {
        const response = await fetch(endpoint, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });
        return await response.json();
    }


    //async put(endpoint, data) {
    //  const response = await fetch(`${this.baseURL}/${endpoint}`, {
    //    method: 'PUT',
    //    headers: {
    //      'Content-Type': 'application/json',
    //    },
    //    body: JSON.stringify(data),
    //  });
    //  return await response.json();
    //}

    //async delete(endpoint) {
    //  const response = await fetch(`${this.baseURL}/${endpoint}`, {
    //    method: 'DELETE',
    //  });
    //  return await response.json();
    //}
}

// Ejemplo de uso
//const api = new ApiClient('https://jsonplaceholder.typicode.com');  // Reemplaza con tu URL de la API

// GET
//api.get('posts/1')
//  .then(data => console.log('GET Result:', data))
//  .catch(error => console.error('GET Error:', error));

// POST
//const postData = {
//  title: 'foo',
//  body: 'bar',
//  userId: 1,
//};

//api.post('posts', postData)
//  .then(data => console.log('POST Result:', data))
//  .catch(error => console.error('POST Error:', error));

// PUT
//const putData = {
//  id: 1,
//  title: 'foo',
//  body: 'bar',
//  userId: 1,
//};

//api.put('posts/1', putData)
//  .then(data => console.log('PUT Result:', data))
//  .catch(error => console.error('PUT Error:', error));

// DELETE
//api.delete('posts/1')
//  .then(data => console.log('DELETE Result:', data))
//  .catch(error => console.error('DELETE Error:', error));
