"# bizApps2" 

:: Постановка задачи :::::::::::::::::::::::::::
> Отсылка к этому решению https://github.com/s-n-o-v/bizApps

Выглядит прям как шаблон, в хорошем смысле этого слова. Сейчас проверим :) Будет задачка если не против. Я ожидал, что будет пустая основа (а тут в ней мусорных пекеджей навалом). Можешь сделать следующие: Удалить все нугет пекеджи. Jquery можешь оставить. Всё что сейчас есть в App_code тоже в топку. Роуты - это не про наше приложение. (хочу видеть на какой странице мы находимся, т.е. чтобы при переходе на страницу был виден полный её урл, включая /default.aspx). CSS добавь какой-нить свой, пусть ультра простой, главное не бутстрапный (этот пекедж тоже в топку, он не нужен для тестового задания). Шапку можешь сделать попроще. (двух страниц более чем достаточно, основная и about достаточно).
Про папку App_code - я не говорю что она должна быть пустая, просто удали все что в ней есть сейчас. Всех этих пяти классов не должно быть в проекте.
Как пример: Можешь попробовать создать пустой ASP.NET webforms проект и посмотреть что там есть.
Осилишь такое?

[28/02/2020] :: Доработка :::::::::::::::::::::::::::


Придумал вам задание одновременно и простое и не очень для человека не работающего с вебформами...
Что нужно сделать:
сейчас у нас на странице есть такой вот контрол: <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false">
Создайте WebControl в CoreLibrary, который бы повторял поведение текущего GridView и замените им текущую разметку <asp:GridView... /> на Default.aspx. Имя для вебконтрола придумайте сами. Свойства вроде HeaderText должны задаваться на Default.aspx страничке и не должны быть захардкожены в контроле.
И поправьте CustomValidator чтобы у него был клиентский код валидации также. Зачем лишний раз напрягать сервер, если мы уже на клиенте знаем что валидация не пройдена.
