fetch('api/products')
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));

let id = sessionStorage.getItem('LocationID')
fetch(`api/locationInventories/${id}`)
    .then(response => response.json())
    .then(data => _addStockColumn(data))

function _displayItems(data) {

    const quantityInput = document.createElement("INPUT");
    const table = document.getElementById('tableBody');

    data.forEach(item => {

        let input1 = quantityInput.cloneNode(false)
        input1.setAttribute("type", "number")

        let tr = table.insertRow();

        let td1 = tr.insertCell(0);
        let textNode1 = document.createTextNode(item.name1)
        td1.appendChild(textNode1);

        let td2 = tr.insertCell(1);
        let textNode2 = document.createTextNode(item.description1)
        td2.appendChild(textNode2);

        let td3 = tr.insertCell(2);
        let textNode3 = document.createTextNode(item.productPrice)
        td3.appendChild(textNode3);

        let td4 = tr.insertCell(3);
        td4.appendChild(input1);

        let td5 = tr.insertCell(4);
        td5.style.display = "none"
        let textNode5 = document.createTextNode(item.id)
        td5.appendChild(textNode5);
    })
}



function _addStockColumn(data) {

    const table = document.getElementById('table1');
    const tableRows = table.rows
    let i = 0
    while (i < tableRows.length-1) {

        let td6 = tableRows[i+1].insertCell(5);
        let textNode6 = document.createTextNode(data[i].stock)
        td6.appendChild(textNode6);
        i++
    }

    
}

function formatOrderIntoObject() {

    let order1 = { LocationId: sessionStorage.getItem('LocationID'), CustomerId: sessionStorage.getItem('CustomerID'), orderItems: [], TotalPrice: 0}
    const table = document.getElementById('table1');
    let i = 1;
    while (i < table.rows.length) {
        let quantity = table.rows[i].cells[3].firstChild.value
        if (quantity > 0) {
            if (quantity > table.rows[i].cells[5].innerHTML) { console.log("No pizza for you"); return }
            else {
                let j = 1
                while (j <= quantity) {
                    let orderItem = makeOrderItem(table.rows[i].cells[4].innerHTML);
                    order1.orderItems.push(orderItem)
                    j++
                    console.log(table.rows[i].cells[2].innerHTML)
                    order1.TotalPrice += parseFloat(table.rows[i].cells[2].innerHTML)
                }
            }
        }
        i++
    }
    submitOrder(order1)
}

function submitOrder(orderObject) {
    fetch('api/orders', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(orderObject)
    })
        //.then(response => response.json())
        //.then(() => {
        //    getItems();
        //    addFirstNameTextbox.value = '';
        //    addLastNameTextbox.value = '';
        //})
        //.catch(error => console.error('Unable to add item.', error));
}



   


function makeOrderItem(ProductId) { return { ProductId }; }