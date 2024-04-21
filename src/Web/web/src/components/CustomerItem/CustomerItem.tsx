import { useState } from 'react';

function CustomerItem({ customer, deleteCustomer, updateCustomer }) {

    const [updatedCustomer, setUpdatedCustomer] = useState(customer);

    const handleChange = (e, field) => {
        setUpdatedCustomer({
            ...updatedCustomer,
            [field]: e.target.value
        });
    };

    return (
        <div className="customer">
            <input
                type="text"
                value={updatedCustomer.firstName}
                onChange={e => handleChange(e, 'firstName')}
            />
            <input
                type="text"
                value={updatedCustomer.lastName}
                onChange={e => handleChange(e, 'lastName')}
            />
            <input
                type="date"
                value={updatedCustomer.birthday}
                onChange={e => handleChange(e, 'birthday')}
            />
            <button onClick={() => deleteCustomer(updatedCustomer.id)}>
                X
            </button>
            <button onClick={() => updateCustomer(updatedCustomer)}>Update</button>
        </div>
    );
}
export default CustomerItem;