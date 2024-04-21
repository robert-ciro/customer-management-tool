import { useState } from "react";
import Contact, { ContactType } from "../../models/Contact";
import ContactItem from "./ContactItem";
import axios from "axios";

function Contacts() {
    const [contacts, setContacts] = useState<Contact[]>([]);
    const [newContact, setNewContact] = useState<Contact>({
        value: "",
        customerId: 0,
        id: 0,
        type: ContactType.Email
    });
    const [errors, setErrors] = useState<undefined>();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>, field: string) => {
        setNewContact({
            ...newContact,
            [field]: e.target.value
        } as Contact);
    };

    const addContact = async (contact: Contact) => {
        setErrors(null!);

        try {
            const response = await axios.post<number>('/contacts', contact);
            contact.id = response.data;
            setContacts([...contacts, contact]);
        } catch (error) {
            setErrors(error.message);
            console.log(error);
        }

    }

    const deleteContact = async (id: number) => {
        setErrors(null!);

        setContacts(contacts.filter(contact => contact.id !== id));
        try {
            await axios.delete<number>(`/contacts/${id}`);
        } catch (error: unknown) {
            setErrors(error.message);
            console.log(error);
        }

    }
    const updateContact = async (contact: Contact) => {
        setErrors(null!);

        try {
            await axios.put(`/contacts/${contact.id}`, contact);

        } catch (error: unknown) {
            setErrors(error.message);
            console.log(error)
        }
    }

    return (
        <div className="contact-list">
            {contacts.map(contact => (
                <ContactItem
                    key={contact.id}
                    contact={contact}
                    deleteContact={deleteContact}
                    updateContact={updateContact}
                />
            ))}
            <select
                onChange={e => handleChange(e, 'type')}>
                {Object.keys(ContactType)
                    .map(value => (
                        <option key={value} value={value} selected={newContact.type === ContactType[value]}>
                            {value}
                        </option>
                    ))}
            </select>
            <input
                type="text"
                placeholder="Value"
                value={newContact?.value}
                onChange={e => handleChange(e, 'value')}
            />
            <input
                type="number"
                placeholder="CustomerId"
                value={newContact?.customerId}
                onChange={e => handleChange(e, 'customerId')}
            />
            <button onClick={() => addContact(newContact)}>Add</button>
            <p>errors:</p>
            <p>{errors}</p>
        </div>
    );
}

export default Contacts;