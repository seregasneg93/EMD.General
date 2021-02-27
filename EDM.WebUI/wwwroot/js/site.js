
fetch("/api/assignments").then((response) => response.json()).then((json) => {
    console.log(json)

    createList(json, document.getElementById('treeView'))
})

function createList(data, root) {

    if (data == null)
        return;

    let ul = document.createElement("ul");
    data.forEach((item) => {
        let li = document.createElement("li")
        let p = document.createElement("p")
        li.textContent = item.name
        let a = document.createElement('a')
        a.text = "+"
        a.href = "/home/create/"+item.id
        //li.appendChild(p)
        li.appendChild(a)
        ul.appendChild(li)
        createList(item, li)
    })

    root.appendChild(ul)
    
}