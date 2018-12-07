# DocumentMaker
This library is used to create `.md` and `.html` document files from `C# `code, a technique very useful
when you
have some data organized in class objects and you want to create a document report for them.

<b>This project is currently under development. First release on Nuget Marketplace
will launch on 10/12/2018 (10th December 2018).</b>

Always remember that the visual effect strictly depends on the CSS files that HTML used to render. 
So your solution might be different from what you see in this guide, unless you use the
same CSS (that's the official GitHub one)

## MDEditor
Main class of the MarkDownEditor library. Attach to this all the information about your
document. 

If you instantiate it base it won't render some of the constructs. In order
to add all the functionalities you must enable what you need after you created it.

This creates a simple `MDEditor`:

```csharp
MDEditor editor = new MDEditor();
```

This way you can create a `MDEditor` with everything enabled:

```csharp
 MDEditor editor = new MDEditor()
                    .EnableExtraEmphasis()
                    .EnableExtraList()
                    .EnableMath()
                    .EnableTable()
                    .EnableTaskList();
```

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

### Example
Code :

```csharp
string bold = TextFormat.Bold("Hello world");
string test = "pippo pluto paperino";
test = TextFormat.ReplaceItalic(test, "pluto");
```
Output :

**Hello world!**

pippo _pluto_ paperino


## IRenderable
Every component (such as all the `Lists`, the `Section`, `Table` and others) implements
the interface `IRenderable`, which contains the method `Render` that returns a string.

If you need to, you can implement it in your own component and add it to a `Section` object,
however this may lead to improper rendering. Try to use the components in this library.

## Section
The core of a document is a Section, with all its nested Sections. This file (readme.md) is
also a MD document, composed by Sections of different heading levels, one nested inside the other.

### Create a Section

```csharp
//Parameters for the constructor are:
//"Section" => title of the section
//2 => heading level
Section secIntroduction = new Section("Introduction", 2);
//default heading level = 1
Section secMain = new Section("Title of the document");  
```

### Adding content to a Section

```csharp
secIntroduction.AddParagraph("Hello world! I am a paragraph!");
secMain.AddParagraph("Following you will find an introduction");
secMain.AddSection(secIntroduction); 
//last instruction puts secIntroduction as nested Section of secMain
```

Using the `Add` method you can add to a Section everything that implements `IRenderable`.

```csharp
secIntroduction.Add(new DotList({"first", "second", "third"}));
```

Check the complete example at the end of the document, where every component will be shown.

## Lists
You can add list of your document of various size and shapes, all behave in similar ways.
You can instantiate them with empty list or with a given array of strings. In both
cases you can add items with `AddItem`.

Code : 
```csharp
DotList dotList = new DotList();  //empty
string[] data = {"a", "b", "c", "d"}
EnumerableList eList = new EnumerableList(data)
dotList.AddItem("e");
dotList.AddItem("f");
```

Output : 

- e
- f

1. a
2. b
3. c
4. d

### Other Lists
Check the final example to see in action:

- `RomanList` (requires *EnableExtraList*): a numbered list with roman numbers (I, II, III, IV, V...)
- `LetterList` (requires *EnableExtraList*): a list with letter, uppercase (A., B., C...) or lowercase (a., b., c...)

### TaskList
Example of a tasklist:

C# : 
```csharp
TaskList taskList = new TaskList({"Not done", "This is not done as well"});
taskList.AddItem("Not even close");
taskList.AddItem("This is done!", true);
taskList.AddItem(new Task("Also this one!", true));
```
MD: 
- [ ] Not done
- [ ] This not done as well
- [ ] Not even close
- [X] This one is actually done!
- [X] Also this one is done! 

## Table
Rendering a table is easy. Main steps are:

1. Instantiate a table setting the (fixed) number of rows and columns
2. (optional) Setting Alignment for the columns (default is LEFT)
3. Setting the headers for the table
4. Fill the data with `SetRow`, `SetCol` or `SetCell`

### Example

C# : 

```csharp
Table table = new Table(2, 3);
table.SetHeaders(new string[] { "A", "B", "C" });
table.SetRow(0, new string[] { "Normal text", TextFormat.Ital("Italic text"), TextFormat.Code("Code text") });
table.SetRow(1, new string[] { "Hello world", "B2", "Lorem ipsum dolor sit amet" });
table.SetAlignement(0, TableAlignment.LEFT);
table.SetAlignement(1, TableAlignment.CENTER);
table.SetAlignement(2, TableAlignment.RIGHT);
```

MD : 

A|B|C
:---|:---:|---:
Normal text|_Italic test_|`Code text`
Hello world|B2|Lorem ipsum dolor sit amet

### Known bug
Changing the TextFormat (bold, italic...) of the _first_ column might compromise
the structure of the whole table. Try to avoid it.


## Template
A template is a piece of text which contains substrings that must be filled with
values. This is useful when you want to inflate your data into a fixed form of string. 

A `Template` object contains `Results`, a List of List of `TemplateItem`, each one being
a simple key-value pair. You might want to change the `TemplateRenderMode`, from `ALL` to
`SINGLE`. In this case only the Result of index `RenderIndex` will be rendered.

Check the final example to see how the template works. 


## Final example
The following is the complete Program found in project MarkDownEditorTest, followed by its
rendered MD and the final effect.

### Code
 ```csharp
 static void Main(string[] args)
        {
            //Create the editor. In this example we are going to use
            //all the possible functionalities, so we must enable all the
            //components
            MDEditor editor = new MDEditor()
                .EnableExtraEmphasis()
                .EnableExtraList()
                .EnableMath()
                .EnableTable()
                .EnableTaskList();

            //Create a section with header level of 1 with the main title
            //All the document will be contained into this first section, but you could
            //create other sections of level 1 as well
            Section H1_MarkDownEditorTest = new Section("MarkDownEditor Test");
            //Be sure to add every section you create to its container
            editor.AddSection(H1_MarkDownEditorTest);

            Section H2_TestParagraph = new Section("Test Paragraph", 2);
            H2_TestParagraph.AddParagraph("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus.");
            H2_TestParagraph.AddParagraph(TextFormat.ReplaceBold("Another paragraph", "Another"));
            H2_TestParagraph.AddHr();
            H2_TestParagraph.AddParagraph("A " + TextFormat.Ital("third") + " paragraph, after the horizontal line");
            H2_TestParagraph.AddQuote("\"So much goes the cat to the lard that she leaves there her little paw\" - Jim Morrison");
            H1_MarkDownEditorTest.AddSection(H2_TestParagraph);

            Section H2_TestLists = new Section("Test Lists", 2);
            Section H3_TestDotList = new Section("Test Dot Lists", 3);
            H3_TestDotList.Add(new DotList(new string[] { "First element", "Second element", TextFormat.Bold("Third element") }));
            H2_TestLists.AddSection(H3_TestDotList);
            Section H3_EnumerableList = new Section("Test Enumerable Lists", 3);
            H3_EnumerableList.Add(new EnumerableList(new string[] { "First element", TextFormat.Code("Second element"), "Third element" }));
            H2_TestLists.AddSection(H3_EnumerableList);
            H1_MarkDownEditorTest.AddSection(H2_TestLists);
            Section H3_RomanList = new Section("Test Roman Lists", 3);
            RomanList romanList = new RomanList();
            for(int i=0;i<10;i++)
            {
                romanList.AddItem(" is the roman number for: [" + (i+1) + "]");
            }
            H3_RomanList.Add(romanList);
            H2_TestLists.AddSection(H3_RomanList);
            Section H3_LetterList = new Section("Test Letter Lists", 3);
            H3_LetterList.Add(new LetterList( new string[] { "First element", "Second element", TextFormat.Bold("Third element") }));
            H2_TestLists.AddSection(H3_LetterList);

            Section H2_TestTable = new Section("Test Table", 2);
            Table table = new Table(2, 3);
            table.SetHeaders(new string[] { "A", "B", "C" });
            table.SetRow(0, new string[] { "Normal text", TextFormat.Ital("Italic text"), TextFormat.Code("Code text") });
            table.SetRow(1, new string[] { "Hello world", "B2", "Lorem ipsum dolor sit amet, consectetuer ecc ecc" });
            table.SetAlignement(0, TableAlignment.LEFT);
            table.SetAlignement(1, TableAlignment.CENTER);
            table.SetAlignement(2, TableAlignment.RIGHT);
            H2_TestTable.Add(table);
            H1_MarkDownEditorTest.AddSection(H2_TestTable);

            Template temp = new Template(text);
            temp.RenderMode = TemplateRenderMode.ALL;
            temp.AddResult();
            temp.AddTemplateItem("sith_name", "Darth Plagueis");
            temp.AddTemplateItem("title", "Wise");
            temp.AddTemplateItem("group", "Jedi");
            temp.AddResult();
            temp.AddTemplateItem("sith_name", "Bob");
            temp.AddTemplateItem("title", "Boozer");
            temp.AddTemplateItem("group", "Anonymous A.");
            H2_TemplateTest.Add(temp);

            Section H2_LinkTest = new Section("Link Test", 2);
            H1_MarkDownEditorTest.AddSection(H2_LinkTest);
            H2_LinkTest.AddParagraph("Link example: " + new MDLink("https://www.google.com", "Google").Render());
            H2_LinkTest.AddParagraph("Link to section: " + new MDLink(H2_TestLists).Render());
            H2_LinkTest.Add(new MDLink(H2_TestTable, "Link to text table"));

            editor.SetCss("C:/Projects/MarkDownEditor/MarkDownEditorTest/style.css", false);

            string mdText = editor.Render();
            string htmlText = editor.RenderToHtml();
            Console.WriteLine(mdText);
            Console.WriteLine("*******************************");
            Console.WriteLine(htmlText);

            editor.RenderToHtmlFile("C:/Projects/MarkDownEditor/MarkDownEditorTest/MarkDownEditor_Text.html");



            Console.ReadLine();
```

### MD text
```
<style> @import url(C:/Projects/MarkDownEditor/MarkDownEditorTest/style.css); </
style>
# MarkDownEditor Test

## Test Paragraph
Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula
eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient
montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu
, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringill
a vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a
, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer ti
ncidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend
tellus.

**Another** paragraph

---
A _third_ paragraph, after the horizontal line

>"So much goes the cat to the lard that she leaves there her little paw" - Jim M
orrison


## Test Lists

### Test Dot Lists
* First element
* Second element
* **Third element**



### Test Enumerable Lists
1. First element
2. `Second element`
3. Third element



### Test Roman Lists
I.  is the roman number for: [1]
III.  is the roman number for: [2]
V.  is the roman number for: [3]
VII.  is the roman number for: [4]
IX.  is the roman number for: [5]
I.  is the roman number for: [6]
III.  is the roman number for: [7]
V.  is the roman number for: [8]
VII.  is the roman number for: [9]
IX.  is the roman number for: [10]



### Test Letter Lists
A. First element
B. Second element
C. **Third element**



## Test Table

A|B|C
:---|:---:|---:
Normal text|_Italic text_|`Code text`
Hello world|B2|Lorem ipsum dolor sit amet, consectetuer ecc ecc





## Template Test
Template: Do you know the tragedy of **[sith_name]** the [title]? It's a story t
he [group] don't tell you

Do you know the tragedy of **Darth Plagueis** the Wise? It's a story the Jedi do
n't tell you

Do you know the tragedy of **Bob** the Boozer? It's a story the Anonymous A. don't tel
l you


## Link Test
Link example: [Google](https://www.google.com)

Link to section: [Test Lists](#Test-Lists)

[Link to text table](#Test-Table)
```



### MD rendered 

# MarkDownEditor Test

## Test Paragraph
Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula
eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient
montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu
, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringill
a vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a
, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer ti
ncidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend
tellus.

**Another** paragraph

---
A _third_ paragraph, after the horizontal line

>"So much goes the cat to the lard that she leaves there her little paw" - Jim M
orrison


## Test Lists

### Test Dot Lists
* First element
* Second element
* **Third element**



### Test Enumerable Lists
1. First element
2. `Second element`
3. Third element



### Test Roman Lists
I.  is the roman number for: [1]
III.  is the roman number for: [2]
V.  is the roman number for: [3]
VII.  is the roman number for: [4]
IX.  is the roman number for: [5]
I.  is the roman number for: [6]
III.  is the roman number for: [7]
V.  is the roman number for: [8]
VII.  is the roman number for: [9]
IX.  is the roman number for: [10]



### Test Letter Lists
A. First element
B. Second element
C. **Third element**



## Test Table

A|B|C
:---|:---:|---:
Normal text|_Italic text_|`Code text`
Hello world|B2|Lorem ipsum dolor sit amet, consectetuer ecc ecc





## Template Test
Template: Do you know the tragedy of **[sith_name]** the [title]? It's a story t
he [group] don't tell you

Do you know the tragedy of **Darth Plagueis** the Wise? It's a story the Jedi do
n't tell you

Do you know the tragedy of **Tom** the Big Boozer? It's a story the AA don't tel
l you


## Link Test
Link example: [Google](https://www.google.com)

Link to section: [Test Lists](#Test-Lists)

[Link to text table](#Test-Table)



