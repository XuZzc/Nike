select * from Salesman 

select Role,[Salesman-Name]   from Salesman where Mobile ='13812345555'

select * from Type 
select * from Goods 
select * from Sales 
select * from SalesDetail 

delete Sales 
select * from Sales,SalesDetail ,Goods,Salesman where Sales .SalesID =SalesDetail .SalesID and SalesDetail .GoodsID =Goods .GoodsID and Sales.SalesmanID =Salesman .SalessmanID and Sales .ReceiptsCode='20180916141620'

select * from Sales,SalesDetail ,Goods,Salesman where Sales .SalesID =SalesDetail .SalesID and SalesDetail .GoodsID =Goods .GoodsID and Sales.SalesmanID =Salesman .SalessmanID and Sales .ReceiptsCode='20180617180622'
update Sales set Amount =Amount-2000 where SalesID =38
select * from Sales,SalesDetail ,Goods,Salesman where Sales .SalesID =SalesDetail .SalesID and SalesDetail .GoodsID =Goods .GoodsID and Sales.SalesmanID =Salesman .SalessmanID and Sales .ReceiptsCode ='20180916125205'
select * from Goods ,Type where Goods .TypeID =Type .TypeID and Goods .BarCode =000000



select * from Type
select * from salesman
delete Salesman where SalessmanID=1
select * from Sales s ,Salesman a where s.SalesmanID =a.SalessmanID and [Salesman-Name] ='韩树清'
select  单笔利润 = sum((Sales.Amount-Goods.StorePrice)*SalesDetail.Quantity) from Sales, SalesDetail,Goods where SalesDetail.GoodsID=Goods.GoodsID and Sales.SalesID='30' 

select * from Sales s ,Salesman a where s.SalesmanID =a.SalessmanID and [Salesman-Name] ='韩树清'
select *from Salesman
select sum(Sales.Amount) from Sales,salesman where Sales.SalesmanID=Salesman.SalessmanID and salesDate between '2017-09-01' and '2018-08-24'  and Sales.SalesmanID='1'
select * from type where parentID=0

select * from Sales s ,Salesman a where s.SalesmanID =a.SalessmanID and [Salesman-Name] ='韩树清'

select  单笔利润 = sum((Sales.Amount-Goods.StorePrice)*SalesDetail.Quantity) from Sales, SalesDetail,Goods where SalesDetail.GoodsID=Goods.GoodsID and Sales.SalesID=30




select sum(Sales.Amount) from Sales,salesman where Sales.SalesmanID=Salesman.SalessmanID and salesDate between '{0}' and '{1}'

select TypeID from type where TypeName='鞋类'
select * from type where parentID=1
select * from Goods,Type where  goods.TypeId=Type.TypeID

select * from Goods,Type where  goods.TypeId=Type.TypeID and BarCode like '8%'
select * from Goods,Type where  goods.TypeId=Type.TypeID and GoodsName like '%酸%'
select * from Goods,Type where  goods.TypeId=Type.TypeID  and ParentID=1 and TypeName ='足球鞋'
select * from Goods,Type where  goods.TypeId=Type.TypeID  and ParentID=1






select sum(Sales.Amount) from Sales,salesman where Sales.SalesmanID=Salesman.SalessmanID and salesDate between '{0}' and '{1}'  and Sales.SalesmanID={2}
select *from Salesman
update Salesman set Wage ='30000' where SalessmanID ='2' 



select * from Goods,Type where  goods.TypeId=Type.TypeID and TypeName='足球鞋'
select TypeID from type where TypeName='鞋类'
select * from Goods,Type where  goods.TypeId=Type.TypeID and StockDate between '2013-06-27 00:00:00'and '2013-06-27 14:00:00'


select * from salesman where role='导购员'



select TypeID from type where TypeName='{0}'
select TypeID from type where TypeName='鞋类'
select distinct  t1.ParentID 父级名称 from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='全部'
select * from type where ParentID='1'
select t1.TypeName  from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='鞋类'
select  t1.ParentID 父级名称 from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='鞋类'
select  t1.ParentID 父级名称 from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='鞋类'
select TypeID from type where TypeName='鞋类'
select * from type where ParentID='1'

select * from Sales,SalesDetail ,Goods,Salesman where Sales .SalesID =SalesDetail .SalesID and SalesDetail .GoodsID =Goods .GoodsID and Sales.SalesmanID =Salesman .SalessmanID and Sales .ReceiptsCode ='20180616024959'
select * from Sales,SalesDetail ,Goods,Salesman where Sales .SalesID =SalesDetail .SalesID and SalesDetail .GoodsID =Goods .GoodsID and Sales.SalesmanID =Salesman .SalessmanID and Sales .ReceiptsCode =''
select * from Sales,SalesDetail ,Goods,Salesman where Sales .SalesID =SalesDetail .SalesID and SalesDetail .GoodsID =Goods .GoodsID and Sales.SalesmanID =Salesman .SalessmanID and Goods .BarCode ='888888'
select * from Goods ,Type where Goods .TypeID =Type .TypeID and BarCode ='213212'

select * from Goods where BarCode ='213212'
update Goods set TypeID ='',GoodsName ='',StorePrice ='',SalePrice ='',Discount ='',StockNum =StockNum+''where GoodsID =''
select TypeID ,TypeName  from Type 

select * from Goods ,Type where Goods .TypeID =Type .TypeID and BarCode ='423423'
select  t1.ParentID 父级名称 from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='鞋类'

select  TypeID from Type where TypeName=''

select t2.TypeName , t1.TypeName  from Type t1,Type t2 where t1.ParentID =t2.TypeID and t1.TypeID='4'
            
select * from Goods,Type where goods.typeid=type.typeid

select ParentID from type where typename='足球鞋'

select * from Goods,Type where goods.typeid=type.typeid and BarCode =''
select * from Goods,Type where goods.typeid=type.typeid and GoodsName  =''
select * from Goods,Type where goods.typeid=type.typeid and StockDate between ''and ''
select * from Goods,Type where goods.typeid=type.typeid and TypeName ='服装'

select * from Goods,Type where goods.typeid=type.typeid and TypeName='足球鞋'
select ParentID from type where typename='鞋类'
select * from Goods,Type where goods.typeid=type.typeid and parentID='1'

select * from Goods,Type where goods.typeid=type.typeid and TypeName='足球鞋'

select * from Goods,Type where goods.typeid=type.typeid and TypeName =''
select  t1.ParentID 父级名称 from Type t1,Type t2 where t1.ParentID =t2.TypeID and t2.TypeName='鞋类'
select * from Goods,Type where goods.typeid=type.typeid and parentID='1'
select * from Goods,Type where goods.typeid=type.typeid and BarCode like '5%'and StockDate between '" + this.dtp_1.Text + "'and '" + this.dtp_2.Text + "'

select * from Goods ,Type where Goods .TypeID =Type .TypeID and BarCode ='213212'
select t2.TypeName , t1.TypeName  from Type t1,Type t2 where t1.ParentID =t2.TypeID and t1.TypeID='4'
select  TypeID from Type where TypeName='足球鞋'
select TypeName from Type where  TypeID='5'

delete from Goods where GoodsID ='8'

select [Salesman-Name],Role  from Salesman where Mobile ='13812345555'


insert into Sales (ReceiptsCode ,SalesDate ,Amount ,SalesmanID ,CashierID )values('','','','','')
                  
 delete SalesDetail where SalesID =''delete Sales where SalesID =''
select * from Sales,SalesDetail ,Goods,Salesman where Sales .SalesID =SalesDetail .SalesID and SalesDetail .GoodsID =Goods .GoodsID and Sales.SalesmanID =Salesman .SalessmanID and Sales .ReceiptsCode ='{0}'
select  * from Sales,SalesDetail ,Goods,Salesman where   SalesDetail .GoodsID =Goods .GoodsID and Sales.SalesmanID =Salesman .SalessmanID and Sales .ReceiptsCode ='20180614112647'

select * from Sales,SalesDetail  WHERE  ReceiptsCode ='20180614112647'
select * from Salesman where [Salesman-Name] ='韩树清'
delete Sales


insert into Sales (ReceiptsCode ,SalesDate ,Amount ,SalesmanID ,CashierID ) values('2018','2013-02-23','983','2','1')
select SalesID  from Sales where  ReceiptsCode='2018'















