export default interface Contact {
    id: number,
    type: ContactType,
    value: string,
    customerId: number
}

enum ContactType {
    Phone = "phone",
    Email = "email",
    Web = "web"
}

export { ContactType }