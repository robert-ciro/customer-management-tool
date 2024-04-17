function TodoItem({ task, deleteTask, toggleCompleted }) {
    function handleChange() {
        toggleCompleted(task.id);
    }

    return (
        <div className="todo-item">
            <input
                type="checkbox"
                checked={task.solved}
                onChange={handleChange}
            />
            <p className={`${task.solved ? 'done' : ''}`}>{task.description}</p>
            <button onClick={() => deleteTask(task.id)}>
                X
            </button>
        </div>
    );
}
export default TodoItem;