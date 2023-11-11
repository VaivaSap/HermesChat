"use strict";

document.getElementById('imageUploadForm').addEventListener('submit', (e) => {
    e.preventDefault();
    const formData = new FormData();
    formData.append('image', document.getElementById('imageInput').files[0]);

    fetch('/upload-image', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            // Handle the response from the backend (e.g., confirmation or error message)
        })
        .catch(error => {
            // Handle any errors
        });
});