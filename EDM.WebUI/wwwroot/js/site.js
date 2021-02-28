
let assignments = []
let assignmentsTree = []
let statusRes = []

fetch("/api/status_res").then(t => t.json()).then(t => statusRes = t)

const loadData = (preSelect = null) =>
    fetch("/api/assignments").then((response) => response.json()).then((json) => {
        console.log(json)

        assignments = []
        assignmentsTree = []

        assignmentsTree = json
        let root = document.getElementById('treeView')
        root.children = []
        root.textContent = ""
        createList(json, document.getElementById('treeView'), preSelect)
    })

function createList(data, root, preSelect = null) {

    if (!data)
        return;

    let ul = document.createElement("ul");
    data.sort(t => t.id).forEach((item) => {
        let li = document.createElement("li")
        let p = document.createElement("a")
        p.textContent = item.name
        p.onclick = (sender) => ItemClick(sender.srcElement, item.id)
        let a = document.createElement('a')
        p.className = "treeItem"
        a.text = "+"
        a.href = "/home/create/"+item.id
        li.appendChild(p)
        li.appendChild(a)
        ul.appendChild(li)
        createList(item.refChildren, li, preSelect)

        assignments.push(item)

        if (preSelect == item.id) {
            p.className = "treeItemSelected"
            selected = p;
            selectedItem = item;
        }
    })

    root.appendChild(ul)
    
}

let selected = {};
let selectedItem = null;

function ItemClick(sender, id) {

    selected.className = "treeItem"
    sender.className = "treeItemSelected"
    selected = sender;
    selectedItem = assignments.find((val) => val.id == id);

    updateInfoPanel()
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

function updateInfoPanel() {
    
    if (selectedItem) {
        document.getElementById("infoPanel").style.visibility = "visible"
        document.getElementById("deleteButton").hidden = false
        //document.getElementById("editLink").hidden = false
    }

    document.getElementById("fieldName").value = selectedItem.name

    document.getElementById("fieldStatus").textContent = statusRes[selectedItem.status].value
    document.getElementById("fieldStatus").className = "status " + statusRes[selectedItem.status].name


    document.getElementById("fieldDescription").value = selectedItem.description
    document.getElementById("fieldDateRegistered").value = formatDate(selectedItem.dateRegistered)
    document.getElementById("fieldDateEnd").value = formatDate(selectedItem.dateEnd)

    document.getElementById("fieldOwnComplexity").value = selectedItem.complexity
    document.getElementById("fieldOwnAchievement").value = selectedItem.achievement

    document.getElementById("fieldComplexity").value =
        computeValue(t => t.complexity, findNode(assignmentsTree, selectedItem.id))
    document.getElementById("fieldAchievement").value =
        computeValue(t => t.achievement, findNode(assignmentsTree, selectedItem.id))


    document.getElementById("editLink").href = "/home/edit/" + selectedItem.id

    //buttons
    document.getElementById("changeStatusBegin").hidden = !(selectedItem.status == 0)
    document.getElementById("changeStatusResume").hidden = !(selectedItem.status == 2)
    document.getElementById("changeStatusComplete").hidden = !((selectedItem.status == 2) || (selectedItem.status == 1))
    document.getElementById("changeStatusStop").hidden = !(selectedItem.status == 1)
}

function computeValue(sel, node) {
    if (node == null)
        return 0;

    if (node.refChildren == null)
        return sel(node)*1;

    return sel(node) * 1 + node.refChildren.map(t => computeValue(sel, t)).reduce((a, b) => a + b, 0)
}

function findNode(arr, id) {

    if (!arr)
        return;

    for (let index = 0; index < arr.length; ++index) {
        if (arr[index].id == id)
            return arr[index];

        let child = findNode(arr[index].refChildren, id);
        if (child && child.id == id)
            return child;
    }

    return null
}

async function deleteSelected() {
    if (confirm("You sure?") && selectedItem) {
        let result = await (await fetch("/api/remove?id=" + selectedItem.id)).json()


        if (result.status == "Ok") {
            await loadData(selectedItem.id);
            updateInfoPanel();
        }
        else
            alert(result.message)
    }
}

async function changeStatus(newStatus) {
    if (selectedItem) {
        let result = await (await fetch("/api/change_status?id=" + selectedItem.id + "&newStatus=" + newStatus)).json()

        if (result.status == "Ok") {
            await loadData(selectedItem.id);
            updateInfoPanel();
        }
        else
            alert(result.message)
    }
}

loadData()