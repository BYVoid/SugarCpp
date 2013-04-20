﻿using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SugarCpp.Compiler
{
    public class TargetCpp : Visitor
    {
        public string Compile(string input)
        {
            input = input.Replace("\r", "");
            ANTLRStringStream Input = new ANTLRStringStream(input);
            SugarCppLexer lexer = new SugarCppLexer(Input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            SugarCppParser parser = new SugarCppParser(tokens);

            AstParserRuleReturnScope<CommonTree, IToken> t = parser.root();
            CommonTree ct = (CommonTree)t.Tree;

            CommonTreeNodeStream nodes = new CommonTreeNodeStream(ct);
            SugarWalker walker = new SugarWalker(nodes);

            Root x = walker.root();
            return x.Accept(this).Render();
        }

        public override Template Visit(Root root)
        {
            Template template = new Template("<list; separator=\"\n\n\">");
            List<Template> list = new List<Template>();
            foreach (var node in root.List)
            {
                list.Add(node.Accept(this));
            }
            template.Add("list", list);
            return template;
        }

        public override Template Visit(Import import)
        {
            Template template = new Template("<list; separator=\"\n\">");
            List<Template> list = new List<Template>();
            foreach (var name in import.NameList)
            {
                Template node = new Template("#include <name>");
                node.Add("name", name);
                list.Add(node);
            }
            template.Add("list", list);
            return template;
        }

        public override Template Visit(FuncDef func_def)
        {
            Template template = new Template("<type> <name>() {\n    <list>\n}");
            template.Add("type", func_def.Type);
            template.Add("name", func_def.Name);
            template.Add("list", func_def.Block.Accept(this));
            return template;
        }

        public override Template Visit(StmtBlock block)
        {
            Template template = new Template("<list; separator=\"\n\">");
            List<Template> list = new List<Template>();
            foreach (var node in block.StmtList)
            {
                Template expr = new Template("<expr>;");
                expr.Add("expr", node.Accept(this));
                list.Add(expr);
            }
            template.Add("list", list);
            return template;
        }

        public override Template Visit(StmtIf stmt_if)
        {
            if (stmt_if.Else == null)
            {
                Template template = new Template("if <cond> {\n    <body>\n}");
                template.Add("cond", stmt_if.Condition.Accept(this));
                template.Add("body", stmt_if.Body.Accept(this));
                return template;
            }
            else
            {
                Template template = new Template("if <cond> {\n    <body1>\n} else {\n    <body2>\n}");
                template.Add("cond", stmt_if.Condition.Accept(this));
                template.Add("body1", stmt_if.Body.Accept(this));
                template.Add("body2", stmt_if.Else.Accept(this));
                return template;
            }

        }

        public override Template Visit(StmtWhile stmt_while)
        {
            Template template = new Template("while <cond> {\n    <body>\n}");
            template.Add("cond", stmt_while.Condition.Accept(this));
            template.Add("body", stmt_while.Body.Accept(this));
            return template;
        }

        public override Template Visit(ExprAssign expr)
        {
            Template template = new Template("<left> = <right>");
            template.Add("left", expr.Left.Accept(this));
            template.Add("right", expr.Right.Accept(this));
            return template;
        }

        public override Template Visit(ExprAlloc expr)
        {
            if (expr.Expr != null)
            {
                Template template = new Template("<type> <name> = <expr>");
                template.Add("type", expr.Type);
                template.Add("name", expr.Name);
                template.Add("expr", expr.Expr.Accept(this));
                return template;
            }
            else
            {
                Template template = new Template("<type> <name>");
                template.Add("type", expr.Type);
                template.Add("name", expr.Name);
                return template;
            }
        }

        public override Template Visit(ExprBin expr)
        {
            Template template = new Template("(<left> <op> <right>)");
            template.Add("left", expr.Left.Accept(this));
            template.Add("right", expr.Right.Accept(this));
            template.Add("op", expr.Op);
            return template;
        }

        public override Template Visit(ExprConst expr)
        {
            return new Template(expr.Text);
        }
    }
}