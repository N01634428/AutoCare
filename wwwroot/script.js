const apiUrl = '/api/customers'; // same origin, no CORS issues

document.addEventListener('DOMContentLoaded', loadCustomers);

document.getElementById('customerForm').addEventListener('submit', async (e) => {
    e.preventDefault();

    const name = document.getElementById('name').value.trim();
    const email = document.getElementById('email').value.trim();
    const phone = document.getElementById('phone').value.trim();

    const response = await fetch(apiUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ fullName: name, email, phone })
    });

    if (response.ok) {
        loadCustomers();
        e.target.reset();
    } else {
        alert('Error adding customer');
    }
});

async function loadCustomers() {
    const res = await fetch(apiUrl);
    const customers = await res.json();

    const list = document.getElementById('customerList');
    list.innerHTML = '';

    customers.forEach(c => {
        const li = document.createElement('li');
        li.innerHTML = `
      <span>${c.fullName} (${c.email})</span>
      <button class="delete-btn" onclick="deleteCustomer(${c.id})">Delete</button>
    `;
        list.appendChild(li);
    });
}

async function deleteCustomer(id) {
    const res = await fetch(`${apiUrl}/${id}`, { method: 'DELETE' });
    if (res.ok) {
        loadCustomers();
    } else {
        alert('Failed to delete customer');
    }
}
