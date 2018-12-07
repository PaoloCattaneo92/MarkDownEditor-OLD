# DocumentMaker
## What is it
This library is used to create `.md` and `.html` document files from `C# `code, a technique very useful
when you
have some data organized in class objects and you want to create a document report for them.

<b><u>This project is currently under development. First release on Nuget Marketplace
will launch on 10/12/2018 (10th December 2018).</u></b>

>Always remember that the visual effect strictly depends on the CSS files that HTML used to render. 

## Installation
TODO release me

### Dependecies
- [Markdig](https://github.com/lunet-io/markdig) (used to render MarkDown in HTML)
- [YamlDotNet](https://github.com/aaubry/YamlDotNet) (used to read templates from YAML)

## Getting started
Create a `Document` object in your code and append all your sections filled with your content. Check the Complete Example at the end of this README or read further for
specific [Features].

A quick sample:

```csharp
var document = new Document();  //create an empty document
document.SetCss("github-markdown.css", embedded: true); //add some style
var helloSec = new Section("Hello world!"); //create a section (heading level 1)
helloSec.AddParagraph("Lorem ipsum and so on"); //add something to the section
document.AddSection(helloSec);     //add the section to the document

Console.WriteLine(document.Render());   //this renders the MarkDown document
Console.WriteLine(document.RenderToHtml());   //this renders the HTML document
```


## Features
With _DocumentMaker_: you can currently create organised documents with:

- A [Document] with layers of [Section]s
- Formatted text with static [TextFormat] class
- Text [Paragraph]
- [Quote]
- [Horizontal break line]
- [Lists] (with dots, numbers, letters, roman letters)
- [Task list]
- Link (to webpages, to sections of the document and even "mailto" links)
- [Table]
- Image
- Mathematical function (work in progress)

Also if you need your custom content you can extend `RenderedObject` in your 
own Object and add it to the document.

# Document components
In this section I will show every component of the library and how to use it. 
Use the list provided in [Features] if you need a specific feature.

## Document
You will add all your data content to this object, that will be rendered in MD or HTML.

```csharp
 Document document = new Document();    //an empty document
```

In order to use some advances functionalities you must enable those with the following syntax:

```csharp
 Document document = new Document()
                    .EnableExtraEmphasis()
                    .EnableExtraList()
                    .EnableMath()
                    .EnableTable()
                    .EnableTaskList();
```

## Section
A Document is made up of Sections of different heading levels, one nested inside the other. An example
of a document structure could be:

| Section A (heading level 1)

| - Section A-A (heading level 2)

| - - Section A-A-A (heading level 3)

| - Section A-B (heading level 2)

| - Section A-C (heading level 2)

...

As you can see each Section can contain nested sub-Sections.

### Create a Section

```csharp
var secMain = new Section("Title of the document");  //default heading level = 1
var secIntroduction = new Section("Introduction", 2);
```

### Adding content to a Section
Using the `Add` method you can add to a Section everything that extends the main class `RenderableObject`, such
as a component that you can find in this guide or a custom one you implemented.

The content of the section will be rendered in the same order it is added. 
You can check it in the `Content` property of the Section.

```csharp
secMain.Add(new DotList() {"A", "B", "C"});
secMain.AddParagraph("Some text");
secMain.Add(myTable);
```

[Back to all Features list](##features)

## TextFormat
This static class contains method used to change
the appearance of text (make it bold, italic and so
on). Each method returns the modified string, formatted from the original one.

These are all the methods with their self-explanatory
names:

```csharp 
- Bold(string original)
- Ital(string original)
- Code(string original)
- Del(string original)
- Apex(string original)
- Sub(string original)
- Mark(string original)
- CodeMultiline(string original, string codename)
```

Also you can use replacing methods, which call the previous one
on substring of the original string. They are:

```csharp
- ReplaceBold(string original, string toBold)
- ReplaceItalic(string original, string toItalic)
- ReplaceCode(string original, string toCode)
- ReplaceDelete(string original, string toDelete)
- ReplaceApex(string original, string toApex)
- ReplaceSub(string original, string toSub)
- ReplaceMark(string original, string toMark)
```

##### Code :

```csharp
string bold = TextFormat.Bold("Hello world");
string testString = "foo bar baz";
testString = TextFormat.ReplaceItalic(testString, "bar");
```
##### Output :

**Hello world!**

foo _bar_ baz

[Back to all Features list](##Features)


## Paragraph
A `Paragraph` object contains a text string. You can add it to the Document using  `AddParagraph`.

##### Code:
```csharp
document.AddParagraph(TextFormat.Bold("Hello ") + world!);
FileInfo file = new FileInfo("C:/textFile.txt");
document.AddParagraph(file);    //all text of this file will be added as a Paragraph
```

##### Output:

**Hello** world!

This text was inside the file parameter and now it stands as a paragraph in the document.

[Back to all Features list](##Features)

## Quote
A quote string is added directly to a Document or to a Section using `AddQuote` method.

##### Code:
```csharp
document.AddQuote("This is a quote")
```

##### Output:

> This is a quote

[Back to all Features list](##Features)

## Horizontal break line
An horizontal break line, also called thematic break, is added directly to the Document using `AddHr` method.

##### Code:
```csharp
document.AddParagraph("First paragraph");
document.AddHr();
document.AddParagraph("Second paragraph");
```

##### Output:

First paragraph

---

Second paragraph


[Back to all Features list](##Features)

## Lists
With _Document Maker_ you can add differnt types of Lists. They all works the same. 

You can add the items in different ways:
- Within the constructor
- Accesing the `Items` property
- Using `AddItem(string item)`
- [Auto mapping list] the properties of an object

Read specific examples to see them in action.



### DotList
##### Code:
```csharp
var dotList = new DotList("A", "B", "C");
```

##### Output:

- A
- B
- C

### LetterList
##### Code:
```csharp
var letterList = new LetterList();
letterList.Type = LetterType.LOWERCASE;
letterList.AddItem("A");
letterList.AddItem("B");
letterList.AddItem("C");

```

##### Output:

a. A
b. B
c. C


#### Remarks
If there are more than 26 items in the list the letter will cycle the alphabet (A-B...-Z-A-B...).

### NumberList
##### Code:
```csharp
var numberList = new NumberList();
string[] itemsSource = new string[]{"A", "B", "C"};
numberList.Items = itemSource;
```

##### Output:

1. A
2. B
3. C

### RomanList
##### Code:
```csharp
var romanList = new RomanList();
string[] itemsSource = new string[]{"A", "B", "C"};
foreach(sring s in itemSource) 
{
    romanList.AddItem(s);
}
```

##### Output:

I. A
II. B
III. C

#### Remarks
Only values up to 10 (X) are supported at the moment.

### Task List
This is a list of `Task` Objects.

##### Code:
```csharp
var task0 = new Task("A", false);
var task1 = new Task("B"); //default is not completed
var task2 = new Task("C", true);
var taskList = new TaskList(task0, task1, task2);
```

##### Output:

- [ ] A
- [ ] B
- [X] C

### Auto mapping list
##### Code:
```csharp
// Example data classes
class FullName {
    public string Name {get;}
    public string Surname {get;}

    public FulLName(string name, string surname) {
        Name = name;
        Surname = surname;
    }

    public override string ToString()  {
        return Name + " " + Surname;
    }
}

class Person {
    public FullName Name;
    public int Age;
    
    public Person(string name, string surname, int age) {
        FullName = new FullName(name, surname);
        Age = age;
    }
}

// Main
var person = new Person("Peter", "Jackson", 57);
var dotList = new DotListAutoMap<Person>(person);
```

##### Output:

- Peter Jackson
- 57

#### Remarks
There are AutoMap version of all the types of lists. 

Each property will be rendered with its `ToString()` method, if you have your own types you should
override those so that you won't end up with simple memory addresses.

[Back to all Features list](##Features)

## Link
Rendering a link is really easy. A Link can have its shown text equals to or different from the 
link itself.

##### Code:
```csharp
var link = new DocLink( "https://github.com/PaoloCattaneo92/DocumentMaker", 
                        "DocumentMaker", 
                        "DocumentMaker github page")
//First parameter is the main link
//Second parameter is the text shown
//Third parameter is the title of the link
```

##### Output:
[DocumentMaker](https://github.com/PaoloCattaneo92/DocumentMaker)

### MailToLink
This specific class is used to render "mailto:" links.
> This will be rendered directly in HTML even in MarkDown rendering

##### Code:
```csharp
var link = new MailToLink("email@address.com", "Contact me");
```

##### Output:
<a href="mailto:email@address.com">Contact me</a>

## Image
TODO write me

## Table
One of the best way to organize your data is in table format. This class really helps you to 
do it in the easiest way.

Let's start with creating an empty table, with a given number of rows and columns.

```csharp
int rows = 2;
int columns = 3;
var table = new Table(rows, columns);
```

> The count for rows and columns are for data alone, excluding the headers

### TableAlignment
You can assign an allignement for all the table or for a specific column only.
```csharp
table.SetAlignement(TableAlignement.CENTER);    //all columns are centered
table.SetAlignement(0, TableAlignement.RIGHT);  //first column is right aligned
```

### Headers
You can set headers with `SetHeaders` method.

```csharp
table.SetHeaders("First column", "Second column", "Third column");
```

### Table content
You can set content inside the table with the following methods:
```csharp 
- SetRow(int row, params string[] contents)
- SetRow(int row, params Object[] contents)
- SetRow(int row, params IRenderable[] contents)
- SetCol(int col, params string[] contents)
- SetCol(int col, params Object[] contents)
- SetCol(int col, params IRenderable[] contents)
- SetCell(int row, int col, string content)
- SetCell(int row, int col, Object content)
- SetCell(int row, int col, IRenderable renderableContent)
``` 
The set values are stored inside the property `ContentMarix`.

### No Content Renderable
If your table has, for some reason, 0 rows, its `NoContentRenderable` object will be rendered instead.

The default "nothing here" value is a paragraph that says _"This table is empty"_ but you can modify it, or
set it to null (in this case nothing will be rendered if the table has no content).

### Automapping table
Using this feature you can parse a list of Objects and automatically map their properties and values in a table.

The table generated in this way will have the Property names as headers.
#### Code
```csharp 
//Class definition
class Rectangle {
    public string Name {get; set;}
    public int Width {get; set;}
    public int Height {get; set;}

    public Rectangle(string name, int width, int height) {
        Name = name;
        Width = width;
        Height = height;
    }
}

//Main
var rec0 = new Rectangle("First", 1, 2);
var rec1 = new Rectangle("Second", 3, 4);
var rec2 = new Rectangle("Third", 5, 6);

var table = new TableAutoMap<Rectangle>(rec0, rec1, rec2);  //it is a params so you can use List, arrays...
table.SetAlignement(TableAlignement.CENTER);
``` 

#### Output
Name|Width|Height
:---:|:---:|:---:
First|1|2
Second|3|4
Third|5|6


[Back to all Features list](##Features)

## Mathematical Function
TODO write me




