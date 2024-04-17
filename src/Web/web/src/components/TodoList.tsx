import { useState } from 'react';
import TodoItem from './TodoItem';
import axios from 'axios';
import Task from '../models/Task';


function TodoList() {
    const [tasks, setTasks] = useState<Task[]>([]);

    const [description, setDescription] = useState('');

    const addTask = async (description: string) => {

        try {
            const model = {
                description: description,
                customerId: 1
            };
            const result = await axios.post <number>('/tasks', model);

            setDescription('');

            const newTask: Task = {
                id: result.data,
                description: description,
                solved: false,
                customerId: 1,
            };

            setTasks([...tasks, newTask]);

        } catch (error: unknown) {
            console.log(error);
        }

    }
    const deleteTask = async (id: number) => {

        setTasks(tasks.filter(task => task.id !== id));

        await axios.delete<number>(`/tasks/${id}`);

    }
    const toggleCompleted = async  (id: number) => {
        setTasks(tasks.map(task => {
            if (task.id === id) {
                return { ...task, solved: !task.solved };
            } else {
                return task;
            }
        }));

        const task = tasks.find(task => task.id == id);
        const status = task?.solved == true ? 'Solve' : 'Unresolved';
        await axios.post<number>(`/tasks/${id}/${status}/`);
    }
    return (
        <div className="todo-list">
            {tasks.map(task => (
                <TodoItem
                    key={task.id}
                    task={task}
                    deleteTask={deleteTask}
                    toggleCompleted={toggleCompleted}
                />
            ))}
            <input
                value={description}
                onChange={e => setDescription(e.target.value)}
            />
            <button onClick={() => addTask(description)}>Add</button>
        </div>
    );
}
export default TodoList;
export { }

