.NET 7.0 / bootstrap 5 / VS 22 / SSMS 19 / MS SQL Express 19

Дополнительно реализована сдача с учетом номинала и количества монет.

https://localhost:7218/User - пользовательский интерфейс

//Бизнес логика
UserController Get: Index()

//Методы

	//Добавление монет в аппарат (количество и общая сумма)
	Condition(int money)
	
	//Покупка передаем id напитка и текущее количество денег в аппарате
	Buy(int id, int money)
	
	//Возвращаем сдачу, с учетом текущего количества монет.
	//Передаем текущее количество денег в аппарате
	Cashback(int money)
	
	//Вспомогательные методы
	
		//Создаем dict для сопоставления номинала и количества выданных монет
		Dictionary<int, int> backCoins(int money)
		
		//Подсчет остатка сдачи. Вход: баланс аппарата, номинал монеты, количество монет calc(59, 10, 3) = 59 - 10 * 3 = 29
		int calc(int balance, int denomination, int count)

https://localhost:7218/Admin?key=qwerty - администриророание

//Бизнес логика
AdminController Get: Index(string key)

//Методы

	//Добавление нового продукта модель Product внутри AdminModelView
	AddProduct(AdminModelView model)

	//Изменение состояния Монеты по ID
	EditCoin(int id, bool active)

	//Изменение данных по товару по ID (завязано на keyup, поэтому price/amount будет null)
	EditProduct(int id, int? price, int? amount)

	//Удаление товара по ID
	DeleteProduct(int id)

//EF DataAccess
DatabaseContext

Краткое описание таблиц:
//Список товаров
Products (Наименование, Цена, Количество в аппарате)

//Список монет
Coins (Номинал, Активность)

//Состояние аппарата
Condition (Всего внесено средств, Количество монет номиналом 1,2,5,10)

//Продажи
Sales (Количество товара, Выручка, Товар, ДатаВремя продажи)