import { useState } from 'react';
import Contact, { ContactType } from '../../models/Contact';

interface Props {
    contact: Contact,
    deleteContact: (id: number) => void,
    updateContact: (contact: Contact) => void
}

export default function ContactItem({ contact, deleteContact, updateContact }: Props) {

    const [updatedContact, setUpdatedContact] = useState(contact);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>, field: string) => {
        setUpdatedContact({
            ...updatedContact,
            [field]: e.target.value
        });
    };

    return (
        <div className="contact">
            <input
                type="text"
                value={updatedContact.value}
                onChange={e => handleChange(e, 'value')}
            />
            <select
                onChange={e => handleChange(e, 'type')}>
                {Object.keys(ContactType).map(key => (
                    <option key={key} value={key} selected={updatedContact.type === ContactType[key]} >
                        {key}
                    </option>
                ))}
            </select>

            <input
                type="number"
                value={updatedContact.customerId}
                onChange={e => handleChange(e, 'customerId')}
            />
            <button onClick={() => deleteContact(updatedContact.id)}>
                X
            </button>
            <button onClick={() => updateContact(updatedContact)}>Update</button>
        </div>
    );
}