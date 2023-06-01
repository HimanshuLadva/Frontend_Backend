import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-todolist',
  templateUrl: './todolist.component.html',
  styleUrls: ['./todolist.component.scss'],
})
export class TodolistComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  todoList: { id: number; data: string; isCompleted: boolean }[] = [];
  showTodo: boolean = false;
  searchArr: any[] = [];
  filter: 'all' | 'active' | 'done' = 'all';
  action: string = '';
  select:string = '';
  searchAction:string = '';
  displayField:boolean = false;

  addTodo(todo: any, search: any) {
    let obj = this.todoList.find((ele) => ele.data === todo.value);
    if (this.todoList.length > 0 && obj !== undefined && todo.value !== '') {
      alert('Entered value is already in the list!!');
    } else if (todo.value) {
      this.showTodo = false;
      search.disabled = true;
      todo.disabled = false;
      this.todoList.push({
        id: this.todoList.length,
        data: todo.value,
        isCompleted: false,
      });
      todo.value = '';
    } else {
      alert('please enter the value');
    }
  }

  get mylist() {
    switch (this.select) {
      case "deleteSelected":
      for (let i = 0; i < this.todoList.length; i++) {
        if (this.todoList[i].isCompleted) {
          this.todoList.splice(i, 1);
          i--;
        }
      }
      this.select = '';
      break;

    case "selectAll":
      for (let i = 0; i < this.todoList.length; i++) {
        this.todoList[i].isCompleted = true;
      }
      this.select = '';
      break;

    case "unselectAll":
      for (let i = 0; i < this.todoList.length; i++) {
        this.todoList[i].isCompleted = false;
      }
      this.select = '';
      break;
    }
    switch (this.action) {
      case 'A-Z':
        this.todoList.sort((a, b) => {
          let fa = a.data.toLowerCase(),
            fb = b.data.toLowerCase();
          if (fa < fb) {
            return -1;
          }
          if (fa > fb) {
            return 1;
          }
          return 0;
        });
        break;

      case 'Z-A':
        this.todoList.sort((a, b) => {
          let fa = a.data.toLowerCase(),
            fb = b.data.toLowerCase();
          if (fa < fb) {
            return 1;
          }
          if (fa > fb) {
            return -1;
          }
          return 0;
        });
        break;
      case 'Newest':
        this.todoList.reverse();
        break;
      case 'Oldest':
        break;
    }
    
   
      if (this.filter === 'all') {
        return this.todoList;
      }
      return this.todoList.filter((item) =>
        this.filter === 'done' ? item.isCompleted==true : item.isCompleted==false
      );
  }

  deleteTodo(id: number) {
    this.todoList = this.todoList.filter((ele) => ele.id != id);
  }

  editTodo(data: any) {
    data.disabled = false;
  }

  saveTodo(data: any, id: number) {
    let ele = this.todoList.filter((ele) => ele.id == id);
    ele[0].data = data.value;
    data.disabled = true;
  }

  searchTodo(todo: any, search: any) {
    search.disabled = false;
    todo.disabled = true;
  }

  searchTodoWord(searchData: any, todo: any) {
    this.showTodo = true;
    this.searchArr = [];
    const inputData = searchData.value;
    let filter = inputData.toUpperCase(),
      txtValue,
      count = 0;

    for (let i = 0; i < this.todoList.length; i++) {
      txtValue = this.todoList[i].data;
      if (txtValue.toUpperCase().indexOf(filter) > -1) {
        this.searchArr.push(this.todoList[i]);

        count++;
      }
    }
    if (count == 0) {
      todo.value = searchData.value;
    }
    this.mylist;
  }

  checkOrNot(id: number) {
    const index = this.todoList.findIndex((ele) => ele.id == id);
    // this.todoList[index].isCompleted = !this.todoList[index].isCompleted;
    if(this.todoList[index].isCompleted) {
      this.todoList[index].isCompleted = false;
    } else {
      this.todoList[index].isCompleted = true;
    }
  }
}
