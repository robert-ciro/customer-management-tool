import { useState, useEffect } from 'react';
import axios from 'axios';
import Customer from '../models/Customer';
import CustomerItem from './CustomerItem';

function ListCustomers() {
    
    const [newCustomer, setNewCustomer] = useState<Customer>();
    const [errors, setErrors] = useState<undefined>();

    const handleChange = (e, field) => {
        setNewCustomer({
            ...newCustomer,
            [field]: e.target.value
        });
    };

    const [customers, setCustomers] = useState<Customer[]>([]);

    const getCustomers = async () => {
        setErrors(null!);
        try {
            const result = await axios.get<Customer[]>('/customers');
            setCustomers(result.data);

        } catch (error: unknown) {
            setErrors(error.message);
            console.log(error);
        }
    }

    const addCustomer = async (customer: Customer) => {
        setErrors(null!);

        try {
            const response = await axios.post<number>('/customers', customer);
            customer.id = response.data;
            setCustomers([...customers, customer]);
        } catch (error: unknown) {
            setErrors(error.message);
            console.log(error);
        }

    }
    const deleteCustomer = async (id: number) => {
        setErrors(null!);

        setCustomers(customers.filter(customer => customer.id !== id));
        try {
            await axios.delete<number>(`/customers/${id}`);
        } catch (error: unknown) {
            setErrors(error.message);
            console.log(error);
        }

    }
    const updateCustomer = async (customer: Customer) => {
        setErrors(null!);

        try {
            await axios.put<number>(`/customers/${customer.id}`, customer);

        } catch (error: unknown) {
            setErrors(error);
            console.log(error)
       }
    }

    useEffect(() => {
        getCustomers();
    }, []);

    return (
        <div className="todo-list">
            {customers.map(customer => (
                <CustomerItem
                    key={customer.id}
                    customer={customer}
                    deleteCustomer={deleteCustomer}
                    updateCustomer={updateCustomer}
                />
            ))}
            <input
                value={newCustomer?.firstName}
                onChange={e => handleChange(e, 'firstName')}
            />
            <input
                value={newCustomer?.lastName}
                onChange={e => handleChange(e, 'lastName')}
            />
            <input
                type="date"
                value={newCustomer?.birthday}
                onChange={e => handleChange(e, 'birthday')}
            />
            <button onClick={() => addCustomer(newCustomer)}>Add</button>
            <p>errors:</p>
            <p>{errors}</p>
        </div>
    );
}
export default ListCustomers;

