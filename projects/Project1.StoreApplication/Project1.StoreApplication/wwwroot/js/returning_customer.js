const uri = 'api/customers';
let userType = sessionStorage.getItem('newOrReturning');
let userPrompt = document.getElementById("userPrompt")
let failureNotice = document.getElementById("failure_notice")
if (userType === 'new') userPrompt.innerHTML = "Enter a unique name to create your new account:"
else userPrompt.innerHTML = "Sign in:"
//will query DB to see if user exists. if so, method will return their id. Otherwise, will return -1.
//Id will then be placed in session storage and Customer dashboard will be loaded with appropriate data.
//For -1, user will be alerted to no bueno sign in.
//var customerExistsResponse;
function confirmExists() {
    const firstName = document.getElementById('add-first-name').value.trim();
    const lastName = document.getElementById('add-last-name').value.trim();

    fetch(`${uri}/firstName=${firstName}&lastName=${lastName}`, {
        method: 'GET'
    })
        .then(response => response.json())
        .then(data => {
            if (data >= 0 && userType === 'returning') { sessionStorage.setItem('CustomerID', data); window.location = "customer_home_page.html"; }
            if (data >= 0 && userType === 'new') failureNotice.innerHTML = "That name has already been taken."
            if (data === -1 && userType === 'new') { addCustomer(firstName, lastName); sessionStorage.setItem('newOrReturning', 'returning'); window.location = "customer_home_page.html" }
            if (data === -1 && userType === 'returning') failureNotice.innerHTML = "We couldn't find you in the system."
            if (data === -2) failureNotice.innerHTML = "Each name can have a max of 50 charactes."
        });
}

function addCustomer(firstName,lastName) {
    
    const customer = {
        FirstName: firstName,
        LastName: lastName
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(customer)
    })
}

