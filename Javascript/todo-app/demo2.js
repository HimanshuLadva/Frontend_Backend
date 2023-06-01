const todoList = document.querySelector(".todo-list-inner");
const todoDiv = document.querySelector(".todo-list");
const addTodo = document.querySelector(".add-todo");
const inputEle = document.querySelector(".input-element");
const inputEnt = document.getElementById("todo-input");
const AllSee = document.querySelector(".allSee");
const searchTodo = document.querySelector(".search-todo");
const listItem = document.querySelectorAll(".item");
const completeSee = document.querySelector(".completedSee");
const activeSee = document.querySelector(".activeSee");
const editButton = document.querySelector(".edit-todo");
const editList = document.querySelector(".edit-list");
const saveList = document.querySelector(".save-list");
const searchLws = document.querySelector(".search-lws");
const action = document.getElementById("action");
const message = document.querySelector(".alert_msg");
const drop1 = document.querySelector("#drop-1");
const drop2 = document.querySelector("#drop-2");
const dlt = document.getElementById("dlt");
const sortUl = document.getElementById("sortUl");
const actionUl = document.getElementById("actionUl");

function myAddArrow1() {
  dlt.classList.add("arrow");
  dlt.classList.remove("arrHover");
  actionUl.classList.remove("hidden1");
}
function myRevArrow1() {
  dlt.classList.remove("arrow");
  dlt.classList.add("arrHover");
  actionUl.classList.add("hidden1");
}

function myAddArrow() {
  action.classList.add("arrow");
  action.classList.remove("arrHover");
  sortUl.classList.remove("hidden1");
}
function myRevArrow() {
  action.classList.remove("arrow");
  action.classList.add("arrHover");
  sortUl.classList.add("hidden1");
}

let listArr = [];
let key = 1;
let count = 0;
let resp = 0;

// display list
function displayList(arr) {
  todoList.textContent = "";

  for (let { value, id, isCompleted } of arr) {
    let html;
    if (
      AllSee.classList.contains("holding") &&
      (isCompleted == "" || isCompleted == "checked")
    ) {
      html = `
      <li class="item" id="${id}"> 
       <input type="checkbox" name="checkBox" id="checkBox" class="checking" onClick="myDisplay(this)" ${isCompleted}/>
       <input type="text" class="list-value" value="${value}" disabled/>
       <button class="edit-list list-btn" onClick="editTodo(this.parentNode)"><i class="fa-solid fa-pen-to-square"></i></button>
       <button class="save-list list-btn" style="display:none"><i class="fa-regular fa-floppy-disk"></i></button>
       <button class="delete-list list-btn" onClick="deleteTodo(this)"><i class="fa-solid fa-trash-can"></i></button>
      </li>
      `;
      todoList.insertAdjacentHTML("beforeend", html);
    } else if (activeSee.classList.contains("holding") && isCompleted == "") {
      html = `
          <li class="item" id="${id}"> 
           <input type="checkbox" name="checkBox" id="checkBox" class="checking" onClick="myDisplay(this)" ${isCompleted}/>
           <input type="text" class="list-value" value="${value}" disabled/>
           <button class="edit-list list-btn" onClick="editTodo(this.parentNode)"><i class="fa-solid fa-pen-to-square"></i></button>
           <button class="save-list list-btn" style="display:none"><i class="fa-regular fa-floppy-disk"></i></button>
           <button class="delete-list list-btn" onClick="deleteTodo(this)"><i class="fa-solid fa-trash-can"></i></button>
          </li>
          `;
      todoList.insertAdjacentHTML("beforeend", html);
    } else if (
      completeSee.classList.contains("holding") &&
      isCompleted == "checked"
    ) {
      html = `
          <li class="item" id="${id}"> 
           <input type="checkbox" name="checkBox" id="checkBox" class="checking" onClick="myDisplay(this)" ${isCompleted}/>
           <input type="text" class="list-value" value="${value}" disabled/>
           <button class="edit-list list-btn" onClick="editTodo(this.parentNode)"><i class="fa-solid fa-pen-to-square"></i></button>
           <button class="save-list list-btn" style="display:none"><i class="fa-regular fa-floppy-disk"></i></button>
           <button class="delete-list list-btn" onClick="deleteTodo(this)"><i class="fa-solid fa-trash-can"></i></button>
          </li>
          `;
      todoList.insertAdjacentHTML("beforeend", html);
    }
  }
}
// show hide
function toggleClass() {
  message.classList.toggle("hidden");
  todoDiv.classList.toggle("hidden");
}
// add list
function addList() {
  const inputData = inputEle.value;
  let obj = listArr.find((arr) => arr.value === inputData);
  if (listArr.length > 0 && obj !== undefined && inputData !== "") {
    alert("Entered value is already in the list!!");
  } else {
    if (inputData) {
      listArr.push({
        value: inputData,
        id: key,
        isCompleted: "",
      });
      displayList(listArr);
      myAction(action.children[0].innerHTML);
      key++;

      if (!message.classList.contains("hidden")) {
        toggleClass();
      }
    } else {
      alert("please enter a value!!!");
    }
  }
  inputEle.value = "";
  searchLws.style.display = "none";
  inputEle.style.display = "";
  searchLws.value = "";
}

// add todo event
inputEnt.addEventListener("keypress", function (e) {
  if (e.key === "Enter") {
    addList();
    e.preventDefault();
  }
});

// Add todo -2
addTodo.addEventListener("click", function (e) {
  addList();
  e.preventDefault();
});

// search todo function
function myFunction() {
  const inputData = searchLws.value;
  const li = todoList.getElementsByTagName("li");
  let input,
    txtValue,
    filter,
    count = 0;
  filter = inputData.toUpperCase();

  for (let i = 0; i < li.length; i++) {
    // input = li[i].getElementsByTagName("input")[0];
    input = li[i].getElementsByClassName("list-value")[0];
    txtValue = input.value;

    if (txtValue.toUpperCase().indexOf(filter) > -1) {
      li[i].style.display = "";
      count++;
    } else {
      li[i].style.display = "none";
    }
  }
  if (count == 0) {
    inputEle.value = searchLws.value;
  }
}

// Search todo
searchTodo.addEventListener("click", function (e) {
  e.preventDefault();
  searchLws.setAttribute("onkeyup", "myFunction()");
  searchLws.style.display = "";
  inputEle.style.display = "none";
});

// edit todo
function editTodo(data) {
  const li = todoList.getElementsByTagName("li");
  const end = data.children[1].value.length;
  data.children[1].setSelectionRange(end, end);
  for (let i = 0; i < li.length; i++) {
    if (li[i].id != data.id) {
      // console.log("ids",li[i].id);
      console.log(li[i].id, listArr[i].id);
      const index = listArr.findIndex((arr) => arr.id == li[i].id);
      listArr[index].value = li[i].children[1].value;
      li[i].children[2].style.display = "";
      li[i].children[3].style.display = "none";
      li[i].children[1].setAttribute("disabled", "true");
    }
    else {
      const index = listArr.findIndex((arr) => arr.id == data.id);
      listArr[index].value = data.children[1].value;
      // // console.log("idmain",li[i].id);
      // const index = listArr.findIndex((arr) => arr.id == li[i].id);
      // // console.log(li[i].children[1].value);
      // listArr[index].value = li[i].children[1].value;
    }
  }

  data.children[1].removeAttribute("disabled");
  data.children[1].focus();
  data.children[2].style.display = "none";
  data.children[3].style.display = "";
  const index = listArr.findIndex((arr) => arr.id == data.id);

  data.children[3].addEventListener("click", function (e) {
    e.preventDefault();
    data.children[1].setAttribute("disabled", "true");
    listArr[index].value = data.children[1].value;
    data.children[2].style.display = "";
    data.children[3].style.display = "none";
  });
}

// Delete Todo
function deleteTodo(ele) {
  for (let i = 0; i < listArr.length; i++) {
    if (listArr[i].id == ele.parentNode.id) {
      listArr.splice(i, 1);
      i--;
      break;
    }
  }
  if (listArr.length == 0) {
    toggleClass();
  }
  displayList(listArr);
  myAction(action.children[0].innerHTML);
}

// checkbox checking test
function myDisplay(data) {
  for (let item of listArr) {
    if (item.id == data.parentNode.id) {
      if (item.isCompleted) {
        item.isCompleted = "";
      } else {
        item.isCompleted = "checked";
      }
    }
  }
  if (!AllSee.classList.contains("holding")) {
    setTimeout(() => {
      data.parentNode.style.display = "none";
    }, 300);
  }
}

// see all todo
AllSee.addEventListener("click", function (e) {
  e.preventDefault();
  completeSee.classList.remove("holding");
  activeSee.classList.remove("holding");
  AllSee.classList.add("holding");
  displayList(listArr);
    myAction(drop2.previousElementSibling.children[0].innerHTML);
});

// see complete todo
completeSee.addEventListener("click", function (e) {
  e.preventDefault();
  completeSee.classList.add("holding");
  activeSee.classList.remove("holding");
  AllSee.classList.remove("holding");
  const li = todoList.getElementsByTagName("li");
  let check;

  for (let i = 0; i < li.length; i++) {
    check = li[i].getElementsByTagName("input")[0];
    check.setAttribute("onclick", "myDisplay(this)");
    if (check.checked) {
      li[i].style.display = "";
    } else {
      li[i].style.display = "none";
    }
  }
  displayList(listArr);
  myAction(drop2.previousElementSibling.children[0].innerHTML);
});

// See Active todo
activeSee.addEventListener("click", function (e) {
  e.preventDefault();
  completeSee.classList.remove("holding");
  activeSee.classList.add("holding");
  AllSee.classList.remove("holding");
  const li = todoList.getElementsByTagName("li");
  let check;
 
  for (let i = 0; i < li.length; i++) {
    check = li[i].getElementsByTagName("input")[0];
    check.setAttribute("onclick", "myDisplay(this)");
    if (check.checked) {
      li[i].style.display = "none";
    } else {
      li[i].style.display = "";
    }
  }
  displayList(listArr);
  myAction(drop2.previousElementSibling.children[0].innerHTML);
});

// sorting todo
function sortList(value = false) {
  const arr = [...listArr];
  value
    ? arr.sort((a, b) => {
        let fa = a.value.toLowerCase(),
          fb = b.value.toLowerCase();

        if (fa < fb) {
          return 1;
        }
        if (fa > fb) {
          return -1;
        }
        return 0;
      })
    : arr.sort((a, b) => {
        let fa = a.value.toLowerCase(),
          fb = b.value.toLowerCase();

        if (fa < fb) {
          return -1;
        }
        if (fa > fb) {
          return 1;
        }
        return 0;
      });
  displayList(arr);
}

function addHold() {
  completeSee.classList.remove("holding");
  activeSee.classList.remove("holding");
  AllSee.classList.add("holding");
}
addHold();

// dropdown -1
function myAction(data) {
  drop2.previousElementSibling.previousElementSibling.innerHTML = data;
  drop2.previousElementSibling.children[0].innerHTML = data;
  console.log("lll", data);
  switch (data) {
    case "Sort":
      sortList();
      break;

    case "A-Z":
      sortList();
      break;

    case "Z-A":
      sortList(true);
      break;

    case "Newest":
      const arr = [...listArr].reverse();
      displayList(arr);
      break;

    case "Oldest":
      displayList(listArr);
      break;
  }
  // sortUl.style.display = "none";
}

function selection(select = false) {
  const li = todoList.getElementsByTagName("li");
  console.log("jjjjj",li);
  // for (let i = 0; i < li.length; i++) {
  //   console.log("himanshu llll");
  //   const select2 = select ? "checked" : "";
  //   if (li[i].style.display == "") {
  //     const index = listArr.findIndex((arr) => arr.id == li[i].id);
  //     listArr[index].isCompleted = select2;
  //   }
  // }
  const select2 = select ? "checked" : "";
  for(let item of listArr) {
    item.isCompleted = select2;
  }
  displayList(listArr);
}
// dropdown -2
function mySelection(value) {
  switch (value) {
    case "Delete Selected":
      for (let i = 0; i < listArr.length; i++) {
        if (listArr[i].isCompleted) {
          listArr.splice(i, 1);
          i--;
        }
      }
      if (listArr.length == 0) {
        toggleClass();
      }
      displayList(listArr);
      break;

    case "Select All":
      selection(true);
      break;

    case "Unselect All":
      selection();
      break;
  }
}
