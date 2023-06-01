var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
// Basic types
var id = 5;
var company = "Himanshu Ladva";
var isPublished = true;
var x = "Hello";
x = "world";
console.log("ID:", id);
var ids = [1, 2, 3, 4, 5];
var arr = [1, true, 'hello'];
// Tuple 
// here is order matter
var person = [1, "hello", true];
// tuple array
var employee;
employee = [
    [1, "H"],
    [2, "B"],
    [3, "C"]
];
// Union  
var pid;
pid = '22';
// enums
var Direction1;
(function (Direction1) {
    Direction1[Direction1["Up"] = 1] = "Up";
    Direction1[Direction1["Down"] = 2] = "Down";
    Direction1[Direction1["Left"] = 3] = "Left";
    Direction1[Direction1["Right"] = 4] = "Right";
})(Direction1 || (Direction1 = {}));
var Direction2;
(function (Direction2) {
    Direction2["Up"] = "Up";
    Direction2["Down"] = "Down";
    Direction2["Left"] = "Left";
    Direction2["Right"] = "Right";
})(Direction2 || (Direction2 = {}));
console.log(Direction2.Up);
var user = {
    id: 1,
    name: 'Himanshu'
};
// type assertion 
var cid = 1;
var customerId = cid;
console.log(customerId);
// function
function addNum(x, y) {
    return x + y;
}
console.log(addNum(1, 2));
// Void
function log(message) {
    console.log(message);
}
log("Hello world");
var user1 = {
    id: 1,
    name: 'Himanshu'
};
user1.name = "Ladva";
var add = function (x, y) { return x + y; };
var sub = function (x, y) { return x - y; };
// Classes
var Person = /** @class */ (function () {
    function Person(id, name) {
        this.id = id;
        this.name = name;
        console.log(123);
    }
    Person.prototype.register = function () {
        return "".concat(this.name, " is now registered");
    };
    return Person;
}());
;
var brad = new Person(1, "Himanshu");
var mike = new Person(2, "Universal King");
console.log(brad.register());
console.log(brad, mike);
// Subclass
var Employee = /** @class */ (function (_super) {
    __extends(Employee, _super);
    function Employee(id, name, position) {
        var _this = _super.call(this, id, name) || this;
        _this.position = position;
        return _this;
    }
    return Employee;
}(Person));
var emp = new Employee(3, "darshit", 'developer');
console.log(emp.register());
// Generic
function getArray(items) {
    return new Array().concat(items);
}
var numArray = getArray([1, 2, 3, 4]);
var strArray = getArray(['him', 'kar', 'dar']);
numArray.push(15);
console.log(numArray);
