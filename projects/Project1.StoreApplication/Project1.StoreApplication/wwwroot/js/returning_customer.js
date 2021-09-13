const uri = 'api/customers';
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
            if (data >= 0) { sessionStorage.setItem('CustomerID', data); window.location = "customer_home_page.html"; }
            else document.getElementById("customerNotFoundText").style.display = "block"; });
    
}

//function actOnCustomersExistence(Number data) {
//    //not sure if identity in t-sql starts at 0 or 1, so just being safe
//    if (data >= 0) { sessionStorage.setItem('CustomerID', data); window.location = "customer_home_page.html"; }
//    else document.getElementById("customerNotFoundText").style.display = "block"; 
//}
