﻿using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SugarCpp.Compiler
{
    public abstract class Expr : Stmt
    {
    }

    public class ExprBracket : Expr
    {
        public Expr Expr;

        public ExprBracket(Expr expr)
        {
            this.Expr = expr;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprAssign : Expr
    {
        public Expr Left, Right;

        public ExprAssign(Expr left, Expr right)
        {
            this.Left = left;
            this.Right = right;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprAlloc : Expr
    {
        public List<string> Name = new List<string>();
        public string Type;
        public List<Expr> ExprList = new List<Expr>();
        public bool IsEqualSign;

        public ExprAlloc(string type, List<string> name, List<Expr> expr_list, bool isEqualSign)
        {
            this.Type = type;
            this.Name = name;
            if (expr_list != null)
            {
                this.ExprList = expr_list;
            }
            this.IsEqualSign = isEqualSign;
        }
        public ExprAlloc(string type, string name, Expr expr, bool isEqualSign)
        {
            this.Type = type;
            this.Name = new List<string> { name };
            this.ExprList = new List<Expr> { expr };
            this.IsEqualSign = isEqualSign;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprBin : Expr
    {
        public Expr Left, Right;
        public string Op;

        public ExprBin(string op, Expr left, Expr right)
        {
            this.Op = op;
            this.Left = left;
            this.Right = right;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprInfix : Expr
    {
        public Expr Left, Right;
        public string Func;

        public ExprInfix(string func, Expr left, Expr right)
        {
            this.Func = func;
            this.Left = left;
            this.Right = right;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprPrefix : Expr
    {
        public Expr Expr;
        public string Op;

        public ExprPrefix(string op, Expr expr)
        {
            this.Expr = expr;
            this.Op = op;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprSuffix : Expr
    {
        public Expr Expr;
        public string Op;

        public ExprSuffix(string op, Expr expr)
        {
            this.Expr = expr;
            this.Op = op;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprDict : Expr
    {
        public Expr Expr;
        public List<Expr> Index = new List<Expr>();

        public ExprDict(Expr expr, List<Expr> index)
        {
            this.Expr = expr;
            if (index != null)
            {
                this.Index = index;
            }
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprCall : Expr
    {
        public Expr Expr;
        public List<Expr> Args = new List<Expr>();
        public List<string> GenericParameter = new List<string>();

        public ExprCall(Expr expr, List<string> generic, List<Expr> args)
        {
            this.Expr = expr;
            if (generic != null)
            {
                this.GenericParameter = generic;
            }
            if (args != null)
            {
                this.Args = args;
            }
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprLambda : Expr
    {
        public Expr Expr;
        public List<ExprAlloc> Args = new List<ExprAlloc>();
        public bool IsRef;

        public ExprLambda(Expr expr, List<ExprAlloc> args, bool isRef)
        {
            this.Expr = expr;
            if (args != null)
            {
                this.Args = args;
            }
            this.IsRef = isRef;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprAccess : Expr
    {
        public Expr Expr;
        public string Op;
        public string Name;

        public ExprAccess(Expr expr, string op, string name)
        {
            this.Expr = expr;
            this.Op = op;
            this.Name = name;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprNewType : Expr
    {
        public List<Expr> Args = new List<Expr>();
        public string ElemType;

        public ExprNewType(string type, List<Expr> args)
        {
            this.ElemType = type;
            if (args != null)
            {
                this.Args = args;
            }
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprNewArray : Expr
    {
        public List<Expr> Args = new List<Expr>();
        public string ElemType;

        public ExprNewArray(string type, List<Expr> args)
        {
            this.ElemType = type;
            if (args != null)
            {
                this.Args = args;
            }
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprCond : Expr
    {
        public Expr Cond;
        public Expr Expr1;
        public Expr Expr2;

        public ExprCond(Expr cond, Expr expr1, Expr expr2)
        {
            this.Cond = cond;
            this.Expr1 = expr1;
            this.Expr2 = expr2;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprTuple : Expr
    {
        public List<Expr> ExprList = new List<Expr>();

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprCast : Expr
    {
        public string Type;
        public Expr Expr;

        public ExprCast(string type, Expr expr)
        {
            this.Type = type;
            this.Expr = expr;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprBlock : Expr
    {
        public List<Stmt> StmtList = new List<Stmt>();

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class ExprList : Expr
    {
        public List<Expr> List = new List<Expr>();

        public ExprList(List<Expr> list)
        {
            if (list != null)
            {
                this.List = list;
            }
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    public enum ConstType
    {
        Ident, String, Number
    }

    public class ExprConst : Expr
    {
        public string Text;
        public ConstType Type;

        public ExprConst(string text, ConstType type)
        {
            this.Text = text;
            this.Type = type;
        }

        public override Template Accept(Visitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
