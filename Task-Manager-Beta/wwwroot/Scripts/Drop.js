const dranggables = document.querySelectorAll('.task')
const taskNames = document.querySelectorAll('.project-column')

dranggables.forEach(dranggable => { 
    dranggable.addEventListener('dragstart', () => {
        dranggable.classList.add('dragging')
    })

    dranggable.addEventListener('dragend', () => {
        dranggable.classList.remove('dragging')
    })
})

taskNames.forEach(taskName => {
    taskName.addEventListener('dragover', e => {
        e.preventDefault()
        const afterElement = getDragAfterElement(taskName, e.clientY)
        const draggable = document.querySelector('.dragging')
        if (afterElement == null) {
            taskName.appendChild(draggable)
        } else {
            taskName.insertBefore(draggable, afterElement)
        }
    })
})

function getDragAfterElement(taskName, y) {
    const draggableElement = [...taskName.querySelectorAll('.task:not(.dragging)')]

    return draggableElement.reduce((closest, child) => {
        const box = child.getBoundingClientRect()
        const offset = y - box.top - box.height / 2
        if (offset < 0 && offset > closest.offset) {
            return { offset: offset, element: child }
        } else {
            return closest
        }
        
    }, {offset: Number.NEGATIVE_INFINITY }).element
}