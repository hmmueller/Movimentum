  grammar Movimentum;

    options {
        language=CSharp2;
    }

    @parser::header {
        using System.Collections.Generic;
        using Movimentum.Model;
        using System.Drawing;
        using System.Globalization;
    }

    @parser::namespace { Movimentum.Parser }

    @lexer::namespace  { Movimentum.Lexer }

    script returns [Script script]
      :                     {{ var ths = new List<Thing>();
                               var sts = new List<Step>();
                            }}
        cfg=config
        ( t=thingdefinition { ths.Add(t); }
        )*
        ( time              {{ var cs = new List<Constraint>(); }}
          ( c=constraint    { cs.Add(c); }
          )*                { sts.Add(new Step(_currentTime, cs)); }
        )*                  { script = new Script(cfg,ths,sts); }
        EOF
      ;

    config returns [Config result]
      : CONFIG '('
              fptu=number  // frames per time unit
        ')'                 { result = new Config(fptu); }
        ';'
      ;

    thingdefinition returns [Thing result]
      : IDENT
        ':'
        s=source            {{ var defs = new Dictionary<string, ConstVector>(); }}
        (anchordefinition[defs]
        )+
        ';'                 { result = new Thing($IDENT.Text, s, defs); }
      ;

    source returns [Image result]
      : FILENAME            { result = ImageFromFile($FILENAME.Text); }
      ;

    anchordefinition [Dictionary<string, ConstVector> defs]
      : n=IDENT
        '='                          {{ ConstVector v = null; }}
        ( c=constvector              { v = c; }
        | i=IDENT '+' c=constvector  { v = ConstAdd($i.Line, defs, $i.Text, c, true); }
        | i=IDENT '-' c=constvector  { v = ConstAdd($i.Line, defs, $i.Text, c, false); }
        )                            { defs[$n.Text] = v; }
      ;

    constvector returns [ConstVector result]
      :                       {{ double x = double.NaN, y = double.NaN; }}
        '['
        ('-' c=constscalar    { x = -c; }
        |    c=constscalar    { x = c; }
        )
        ','
        ('-' c=constscalar    { y = -c; }
        |    c=constscalar    { y = c; }
        )
        ( ','                 {{ double z = double.NaN; }}
          ('-' c=constscalar  { z = -c; }
          |    c=constscalar  { z = c; }
          )                   { result = new ConstVector(x,y,z); }
		|                     { result = new ConstVector(x,y); }
		)
        ']'                 
      ;

    constscalar returns [double result]
      : n=number            { result = n; }
      | c=constvector       
        ( X                 { result = c.X; }
        | Y                 { result = c.Y; }
        | Z                 { result = c.Z; }
        )
      ;

    time
      : '@' n=number        { _currentTime = n; }
      | '@' '+' n=number    { _currentTime += n; }
      ;

    constraint returns [Constraint result]
      : a=anchor
        '='
        v=vectorexpr
        ';'                 { result = new VectorEqualityConstraint(a, v); }
      | i=IDENT
        '='
        s=scalarexpr
        ';'                 { result = new ScalarEqualityConstraint($i.Text, s); }
      | i=IDENT             {{ ScalarInequalityOperator op = null; }}
        ( '<'               { op = ScalarInequalityOperator.LE; }
        | '>'               { op = ScalarInequalityOperator.GE; }
        | '<='              { op = ScalarInequalityOperator.LT; }
        | '>='              { op = ScalarInequalityOperator.GT; }
        )                   
        s=scalarexpr        
        ';'                 { result = new ScalarInequalityConstraint($i.Text, op, s); }
      ;

    vectorexpr returns [VectorExpr result]
      : v=vectorexpr2       { result = v; }
        (                   {{ BinaryVectorOperator op = null; }}
          ( '+'             {{ op = BinaryVectorOperator.PLUS; }}
          | '-'             {{ op = BinaryVectorOperator.MINUS; }}
          )                 
          v=vectorexpr2     { result = new BinaryVectorExpr(result, op, v); }
        )*                  
      ;                     
                            
    vectorexpr2 returns [VectorExpr result]
      : v=vectorexpr3       { result = v; }
        ( ROTATE
          '('
          s=scalarexpr      { result = new VectorScalarExpr(result, VectorScalarOperator.ROTATE, s); }
          ')'
		| '*'
		  s=scalarexpr4     { result = new VectorScalarExpr(result, VectorScalarOperator.TIMES, s); }
        )*
      ;

    vectorexpr3 returns [VectorExpr result]
      : '-' v=vectorexpr4   { result = new UnaryVectorExpr(UnaryVectorOperator.MINUS, v); }
      | v=vectorexpr4       { result = v; }
      ;

    vectorexpr4 returns [VectorExpr result]
      : INTEGRAL
        '('
        v=vectorexpr        { result = new UnaryVectorExpr(UnaryVectorOperator.INTEGRAL, v); }
        ')'                 
      | DIFFERENTIAL        
        '('                 
        v=vectorexpr        { result = new UnaryVectorExpr(UnaryVectorOperator.DIFFERENTIAL, v); }
        ')'                 
      | v=vectorexpr5       { result = v; }
      ;

    vectorexpr5 returns [VectorExpr result]
      : '('
        e=vectorexpr        { result = e; }
        ')'                 
      | v=vector            { result = v; }
      | a=anchor            { result = a; }
      | IDENT               { result = new VectorVariable($IDENT.Text); }
      | '_'                 { result = new VectorVariable("#" + _anonymousVarCt++); }
    ;                       
                            
    vector returns [Vector result]
      : '['
        s1=scalarexpr
        ','
        s2=scalarexpr
        ( ','
          s3=scalarexpr
		)?
        ']'                 { result = new Vector(s1, s2, s3 ?? new Constant(0)); }
      ;

    anchor returns [Anchor result]
      :  th=thing 
         '.' 
         IDENT              { result = new Anchor(th, $IDENT.Text); }
      ;

    thing returns [string result]
      : IDENT               { result = $IDENT.Text; }
      ;

    scalarexpr returns [ScalarExpr result]
      : s=scalarexpr2       { result = s; }
        (                   {{ BinaryScalarOperator op = null; }}
          ( '+'             {{ op = BinaryScalarOperator.PLUS; }}
          | '-'             {{ op = BinaryScalarOperator.MINUS; }}
          )                 
          s=scalarexpr2     { result = new BinaryScalarExpr(result, op, s); }
        )*
      ;

    scalarexpr2 returns [ScalarExpr result]
      : s=scalarexpr3       { result = s; }
        (                   {{ BinaryScalarOperator op = null; }}
          ( '*'             {{ op = BinaryScalarOperator.TIMES; }}
          | '/'             {{ op = BinaryScalarOperator.DIVIDE; }}
          )                 
          s=scalarexpr3     { result = new BinaryScalarExpr(result, op, s); }
        )*                  
      ;                     

    scalarexpr3 returns [ScalarExpr result]
      : '-' s=scalarexpr4   { result = new UnaryScalarExpr(UnaryScalarOperator.MINUS, s); }
      | s=scalarexpr4       { result = s; }
      ;

    scalarexpr4 returns [ScalarExpr result]
	  : s=scalarexpr5
	    ( '^2'              { result = new UnaryScalarExpr(UnaryScalarOperator.SQUARED, s); }
		| '^3'              { result = new UnaryScalarExpr(UnaryScalarOperator.CUBED, s); }
		|                   { result = s; }
		)
	  | SQRT
	    '('
        s=scalarexpr5       { result = new UnaryScalarExpr(UnaryScalarOperator.SQUAREROOT, s); }
	    ')'
	  ;

    scalarexpr5 returns [ScalarExpr result]
      : s=scalarexpr5Ambiguous
                            { result = s; }
      | INTEGRAL '('        
          s=scalarexpr      
          ')'               { result = new UnaryScalarExpr(UnaryScalarOperator.INTEGRAL, s); }
      | DIFFERENTIAL '('    
          s=scalarexpr      
          ')'               { result = new UnaryScalarExpr(UnaryScalarOperator.DIFFERENTIAL, s); }
      | ANGLE '('           
          v1=vectorexpr     
          ','               
          v2=vectorexpr
          ')'               { result = new BinaryScalarVectorExpr(v1, BinaryScalarVectorOperator.ANGLE, v2); }
      | n=number            { result = new Constant(n); }
      | IDENT               { result = new ScalarVariable($IDENT.Text); }
      | '_'                 { result = new ScalarVariable("_" + _anonymousVarCt++); }
      // | thing COLOR      { result = ...; }
      | T                   { result = new T(); }
      | IV                  { result = new IV(); }
      ;                     

   scalarexpr5Ambiguous returns [ScalarExpr result]
                    options { backtrack = true; }
     :  v=vectorexpr5
       ( X                  { result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.X); }
       | Y                  { result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.Y); }
       | Z                  { result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.Z); }
       | LENGTH             { result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.LENGTH); }
       )                
     | '('              
       s=scalarexpr         { result = s; }
       ')'
     ;

   number returns [double value]
     : NUMBER               { value = double.Parse($NUMBER.Text, CultureInfo.InvariantCulture); }
     ;

// -------

    CONFIG : '.config';

    X :  '.x';
    Y :  '.y';
    Z :  '.z';
    T :  '.t';
    IV :  '.iv';

    SQRT         : '.q' | '.sqrt';
    ROTATE       : '.r' | '.rotate';
    INTEGRAL     : '.i' | '.integral';
    DIFFERENTIAL : '.d' | '.differential';
    ANGLE        : '.a' | '.angle';
    COLOR        : '.c' | '.color';
    LENGTH       : '.l' | '.LENGTH';

    NUMBER
      : ('0'..'9')+
         ( '.'
           ('0'..'9')*
         )?
        ( ('E'|'e')
          ('-')?
          ('0'..'9')+
        )?
      ;

    WHITESPACE
      : ( '\t' | ' ' | '\r' | '\n'| '\u000C' )+     { $channel = HIDDEN; }
      ;

    COMMENT
      : '/' '/' .* ( '\r' | '\n' )                  { $channel = HIDDEN; }
      ;

    IDENT
      : ('a'..'z'|'A'..'Z')('a'..'z'|'A'..'Z'|'0'..'9'|'_')*
    //  : (__LETTER__)
    //    (__LETTER__|'_'|__NUMBER__)*
      ;

    FILENAME
      :  '\''
        (~('\''))*
        '\''
      ;
