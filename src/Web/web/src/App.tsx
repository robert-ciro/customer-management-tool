import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import DataSample from './components/DataSample'
import TodoList from './components/TodoList'
import axios from 'axios';

import './App.css';
import ListCustomers from './components/ListCustomers';
import Contacts from './components/Contacts/Contacts';

axios.defaults.baseURL = 'https://localhost:7179/api/';
axios.defaults.headers.post['Content-Type'] = 'application/json';

function App() {
    return (
        <div>
            <nav>
                <ul>
                    <li><a href="/sample-data">Sample Data</a></li>
                    <li><a href="/tasks">Tasks</a></li>
                    <li><a href="/customers">Customers</a></li>
                    <li><a href="/contacts">Contacts</a></li>
                </ul>
            </nav>

            <BrowserRouter>
                <Routes>
                    <Route path="/sample-data" Component={DataSample} />
                    <Route path="/tasks" Component={TodoList} />
                    <Route path="/customers" Component={ListCustomers} />
                    <Route path="/contacts" Component={Contacts} />
                </Routes>
            </BrowserRouter>
        </div>
    );
}

export default App;