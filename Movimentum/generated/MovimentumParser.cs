// $ANTLR 3.1.1 Movimentum.g 2012-04-24 20:58:33
// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162
namespace  Movimentum.Parser 
{

        using System.Collections.Generic;
        using Movimentum.Model;
        using System.Drawing;
        using System.Globalization;
    

using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;

using IDictionary	= System.Collections.IDictionary;
using Hashtable 	= System.Collections.Hashtable;

public partial class MovimentumParser : Parser
{
    public static readonly string[] tokenNames = new string[] 
	{
        "<invalid>", 
		"<EOR>", 
		"<DOWN>", 
		"<UP>", 
		"CONFIG", 
		"IDENT", 
		"FILENAME", 
		"X", 
		"Y", 
		"Z", 
		"ROTATE", 
		"INTEGRAL", 
		"DIFFERENTIAL", 
		"SQRT", 
		"ANGLE", 
		"T", 
		"IV", 
		"LENGTH", 
		"NUMBER", 
		"COLOR", 
		"WHITESPACE", 
		"COMMENT", 
		"'('", 
		"')'", 
		"';'", 
		"':'", 
		"'='", 
		"'+'", 
		"'-'", 
		"'['", 
		"','", 
		"']'", 
		"'@'", 
		"'<'", 
		"'>'", 
		"'<='", 
		"'>='", 
		"'*'", 
		"'_'", 
		"'.'", 
		"'/'", 
		"'^2'", 
		"'^3'"
    };

    public const int T__42 = 42;
    public const int T__40 = 40;
    public const int T__41 = 41;
    public const int T__29 = 29;
    public const int T__28 = 28;
    public const int ANGLE = 14;
    public const int T__27 = 27;
    public const int T__26 = 26;
    public const int T__25 = 25;
    public const int T__24 = 24;
    public const int T__23 = 23;
    public const int T__22 = 22;
    public const int NUMBER = 18;
    public const int T = 15;
    public const int WHITESPACE = 20;
    public const int INTEGRAL = 11;
    public const int ROTATE = 10;
    public const int SQRT = 13;
    public const int EOF = -1;
    public const int COLOR = 19;
    public const int Y = 8;
    public const int LENGTH = 17;
    public const int X = 7;
    public const int Z = 9;
    public const int T__30 = 30;
    public const int CONFIG = 4;
    public const int T__31 = 31;
    public const int T__32 = 32;
    public const int T__33 = 33;
    public const int T__34 = 34;
    public const int T__35 = 35;
    public const int T__36 = 36;
    public const int T__37 = 37;
    public const int FILENAME = 6;
    public const int T__38 = 38;
    public const int T__39 = 39;
    public const int IV = 16;
    public const int IDENT = 5;
    public const int COMMENT = 21;
    public const int DIFFERENTIAL = 12;

    // delegates
    // delegators



        public MovimentumParser(ITokenStream input)
    		: this(input, new RecognizerSharedState()) {
        }

        public MovimentumParser(ITokenStream input, RecognizerSharedState state)
    		: base(input, state) {
            InitializeCyclicDFAs();

             
        }
        

    override public string[] TokenNames {
		get { return MovimentumParser.tokenNames; }
    }

    override public string GrammarFileName {
		get { return "Movimentum.g"; }
    }



    // $ANTLR start "script"
    // Movimentum.g:18:5: script returns [Script script] : cfg= config (t= thingdefinition )* ( time (c= constraint )* )* EOF ;
    public Script script() // throws RecognitionException [1]
    {   
        Script script = default(Script);

        Config cfg = default(Config);

        Thing t = default(Thing);

        Constraint c = default(Constraint);


        try 
    	{
            // Movimentum.g:19:7: (cfg= config (t= thingdefinition )* ( time (c= constraint )* )* EOF )
            // Movimentum.g:19:29: cfg= config (t= thingdefinition )* ( time (c= constraint )* )* EOF
            {
            	 var ths = new List<Thing>();
            	                               var sts = new List<Step>();
            	                            
            	PushFollow(FOLLOW_config_in_script124);
            	cfg = config();
            	state.followingStackPointer--;
            	if (state.failed) return script;
            	// Movimentum.g:23:9: (t= thingdefinition )*
            	do 
            	{
            	    int alt1 = 2;
            	    int LA1_0 = input.LA(1);

            	    if ( (LA1_0 == IDENT) )
            	    {
            	        alt1 = 1;
            	    }


            	    switch (alt1) 
            		{
            			case 1 :
            			    // Movimentum.g:23:11: t= thingdefinition
            			    {
            			    	PushFollow(FOLLOW_thingdefinition_in_script138);
            			    	t = thingdefinition();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return script;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	   ths.Add(t); 
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop1;
            	    }
            	} while (true);

            	loop1:
            		;	// Stops C# compiler whining that label 'loop1' has no statements

            	// Movimentum.g:25:9: ( time (c= constraint )* )*
            	do 
            	{
            	    int alt3 = 2;
            	    int LA3_0 = input.LA(1);

            	    if ( (LA3_0 == 32) )
            	    {
            	        alt3 = 1;
            	    }


            	    switch (alt3) 
            		{
            			case 1 :
            			    // Movimentum.g:25:11: time (c= constraint )*
            			    {
            			    	PushFollow(FOLLOW_time_in_script163);
            			    	time();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return script;
            			    	 var cs = new List<Constraint>(); 
            			    	// Movimentum.g:26:11: (c= constraint )*
            			    	do 
            			    	{
            			    	    int alt2 = 2;
            			    	    int LA2_0 = input.LA(1);

            			    	    if ( (LA2_0 == IDENT) )
            			    	    {
            			    	        alt2 = 1;
            			    	    }


            			    	    switch (alt2) 
            			    		{
            			    			case 1 :
            			    			    // Movimentum.g:26:13: c= constraint
            			    			    {
            			    			    	PushFollow(FOLLOW_constraint_in_script194);
            			    			    	c = constraint();
            			    			    	state.followingStackPointer--;
            			    			    	if (state.failed) return script;
            			    			    	if ( state.backtracking == 0 ) 
            			    			    	{
            			    			    	   cs.Add(c); 
            			    			    	}

            			    			    }
            			    			    break;

            			    			default:
            			    			    goto loop2;
            			    	    }
            			    	} while (true);

            			    	loop2:
            			    		;	// Stops C# compiler whining that label 'loop2' has no statements

            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	   sts.Add(new Step(_currentTime, cs)); 
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop3;
            	    }
            	} while (true);

            	loop3:
            		;	// Stops C# compiler whining that label 'loop3' has no statements

            	if ( state.backtracking == 0 ) 
            	{
            	   script = new Script(cfg,ths,sts); 
            	}
            	Match(input,EOF,FOLLOW_EOF_in_script269); if (state.failed) return script;

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return script;
    }
    // $ANTLR end "script"


    // $ANTLR start "config"
    // Movimentum.g:32:5: config returns [Config result] : CONFIG '(' fptu= number ')' ';' ;
    public Config config() // throws RecognitionException [1]
    {   
        Config result = default(Config);

        double fptu = default(double);


        try 
    	{
            // Movimentum.g:33:7: ( CONFIG '(' fptu= number ')' ';' )
            // Movimentum.g:33:9: CONFIG '(' fptu= number ')' ';'
            {
            	Match(input,CONFIG,FOLLOW_CONFIG_in_config298); if (state.failed) return result;
            	Match(input,22,FOLLOW_22_in_config300); if (state.failed) return result;
            	PushFollow(FOLLOW_number_in_config318);
            	fptu = number();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,23,FOLLOW_23_in_config330); if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = new Config(fptu); 
            	}
            	Match(input,24,FOLLOW_24_in_config358); if (state.failed) return result;

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "config"


    // $ANTLR start "thingdefinition"
    // Movimentum.g:39:5: thingdefinition returns [Thing result] : IDENT ':' s= source ( anchordefinition[defs] )+ ';' ;
    public Thing thingdefinition() // throws RecognitionException [1]
    {   
        Thing result = default(Thing);

        IToken IDENT1 = null;
        Image s = default(Image);


        try 
    	{
            // Movimentum.g:40:7: ( IDENT ':' s= source ( anchordefinition[defs] )+ ';' )
            // Movimentum.g:40:9: IDENT ':' s= source ( anchordefinition[defs] )+ ';'
            {
            	IDENT1=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_thingdefinition387); if (state.failed) return result;
            	Match(input,25,FOLLOW_25_in_thingdefinition397); if (state.failed) return result;
            	PushFollow(FOLLOW_source_in_thingdefinition409);
            	s = source();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	 var defs = new Dictionary<string, ConstVector>(); 
            	// Movimentum.g:43:9: ( anchordefinition[defs] )+
            	int cnt4 = 0;
            	do 
            	{
            	    int alt4 = 2;
            	    int LA4_0 = input.LA(1);

            	    if ( (LA4_0 == IDENT) )
            	    {
            	        alt4 = 1;
            	    }


            	    switch (alt4) 
            		{
            			case 1 :
            			    // Movimentum.g:43:10: anchordefinition[defs]
            			    {
            			    	PushFollow(FOLLOW_anchordefinition_in_thingdefinition433);
            			    	anchordefinition(defs);
            			    	state.followingStackPointer--;
            			    	if (state.failed) return result;

            			    }
            			    break;

            			default:
            			    if ( cnt4 >= 1 ) goto loop4;
            			    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            		            EarlyExitException eee =
            		                new EarlyExitException(4, input);
            		            throw eee;
            	    }
            	    cnt4++;
            	} while (true);

            	loop4:
            		;	// Stops C# compiler whinging that label 'loop4' has no statements

            	Match(input,24,FOLLOW_24_in_thingdefinition455); if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = new Thing(IDENT1.Text, s, defs); 
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "thingdefinition"


    // $ANTLR start "source"
    // Movimentum.g:48:5: source returns [Image result] : FILENAME ;
    public Image source() // throws RecognitionException [1]
    {   
        Image result = default(Image);

        IToken FILENAME2 = null;

        try 
    	{
            // Movimentum.g:49:7: ( FILENAME )
            // Movimentum.g:49:9: FILENAME
            {
            	FILENAME2=(IToken)Match(input,FILENAME,FOLLOW_FILENAME_in_source502); if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = ImageFromFile(FILENAME2.Text); 
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "source"


    // $ANTLR start "anchordefinition"
    // Movimentum.g:52:5: anchordefinition[Dictionary<string, ConstVector> defs] : n= IDENT '=' (c= constvector | i= IDENT '+' c= constvector | i= IDENT '-' c= constvector ) ;
    public void anchordefinition(Dictionary<string, ConstVector> defs) // throws RecognitionException [1]
    {   
        IToken n = null;
        IToken i = null;
        ConstVector c = default(ConstVector);


        try 
    	{
            // Movimentum.g:53:7: (n= IDENT '=' (c= constvector | i= IDENT '+' c= constvector | i= IDENT '-' c= constvector ) )
            // Movimentum.g:53:9: n= IDENT '=' (c= constvector | i= IDENT '+' c= constvector | i= IDENT '-' c= constvector )
            {
            	n=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_anchordefinition544); if (state.failed) return ;
            	Match(input,26,FOLLOW_26_in_anchordefinition554); if (state.failed) return ;
            	 ConstVector v = null; 
            	// Movimentum.g:55:9: (c= constvector | i= IDENT '+' c= constvector | i= IDENT '-' c= constvector )
            	int alt5 = 3;
            	int LA5_0 = input.LA(1);

            	if ( (LA5_0 == 29) )
            	{
            	    alt5 = 1;
            	}
            	else if ( (LA5_0 == IDENT) )
            	{
            	    int LA5_2 = input.LA(2);

            	    if ( (LA5_2 == 27) )
            	    {
            	        alt5 = 2;
            	    }
            	    else if ( (LA5_2 == 28) )
            	    {
            	        alt5 = 3;
            	    }
            	    else 
            	    {
            	        if ( state.backtracking > 0 ) {state.failed = true; return ;}
            	        NoViableAltException nvae_d5s2 =
            	            new NoViableAltException("", 5, 2, input);

            	        throw nvae_d5s2;
            	    }
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return ;}
            	    NoViableAltException nvae_d5s0 =
            	        new NoViableAltException("", 5, 0, input);

            	    throw nvae_d5s0;
            	}
            	switch (alt5) 
            	{
            	    case 1 :
            	        // Movimentum.g:55:11: c= constvector
            	        {
            	        	PushFollow(FOLLOW_constvector_in_anchordefinition595);
            	        	c = constvector();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return ;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   v = c; 
            	        	}

            	        }
            	        break;
            	    case 2 :
            	        // Movimentum.g:56:11: i= IDENT '+' c= constvector
            	        {
            	        	i=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_anchordefinition624); if (state.failed) return ;
            	        	Match(input,27,FOLLOW_27_in_anchordefinition626); if (state.failed) return ;
            	        	PushFollow(FOLLOW_constvector_in_anchordefinition630);
            	        	c = constvector();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return ;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   v = ConstAdd(i.Line, defs, i.Text, c, true); 
            	        	}

            	        }
            	        break;
            	    case 3 :
            	        // Movimentum.g:57:11: i= IDENT '-' c= constvector
            	        {
            	        	i=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_anchordefinition647); if (state.failed) return ;
            	        	Match(input,28,FOLLOW_28_in_anchordefinition649); if (state.failed) return ;
            	        	PushFollow(FOLLOW_constvector_in_anchordefinition653);
            	        	c = constvector();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return ;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   v = ConstAdd(i.Line, defs, i.Text, c, false); 
            	        	}

            	        }
            	        break;

            	}

            	if ( state.backtracking == 0 ) 
            	{
            	   defs[n.Text] = v; 
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "anchordefinition"


    // $ANTLR start "constvector"
    // Movimentum.g:61:5: constvector returns [ConstVector result] : '[' ( '-' c= constscalar | c= constscalar ) ',' ( '-' c= constscalar | c= constscalar ) ( ',' ( '-' c= constscalar | c= constscalar ) | ) ']' ;
    public ConstVector constvector() // throws RecognitionException [1]
    {   
        ConstVector result = default(ConstVector);

        double c = default(double);


        try 
    	{
            // Movimentum.g:62:7: ( '[' ( '-' c= constscalar | c= constscalar ) ',' ( '-' c= constscalar | c= constscalar ) ( ',' ( '-' c= constscalar | c= constscalar ) | ) ']' )
            // Movimentum.g:62:31: '[' ( '-' c= constscalar | c= constscalar ) ',' ( '-' c= constscalar | c= constscalar ) ( ',' ( '-' c= constscalar | c= constscalar ) | ) ']'
            {
            	 double x = double.NaN, y = double.NaN; 
            	Match(input,29,FOLLOW_29_in_constvector756); if (state.failed) return result;
            	// Movimentum.g:64:9: ( '-' c= constscalar | c= constscalar )
            	int alt6 = 2;
            	int LA6_0 = input.LA(1);

            	if ( (LA6_0 == 28) )
            	{
            	    alt6 = 1;
            	}
            	else if ( (LA6_0 == NUMBER || LA6_0 == 29) )
            	{
            	    alt6 = 2;
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            	    NoViableAltException nvae_d6s0 =
            	        new NoViableAltException("", 6, 0, input);

            	    throw nvae_d6s0;
            	}
            	switch (alt6) 
            	{
            	    case 1 :
            	        // Movimentum.g:64:10: '-' c= constscalar
            	        {
            	        	Match(input,28,FOLLOW_28_in_constvector767); if (state.failed) return result;
            	        	PushFollow(FOLLOW_constscalar_in_constvector771);
            	        	c = constscalar();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return result;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   x = -c; 
            	        	}

            	        }
            	        break;
            	    case 2 :
            	        // Movimentum.g:65:14: c= constscalar
            	        {
            	        	PushFollow(FOLLOW_constscalar_in_constvector793);
            	        	c = constscalar();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return result;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   x = c; 
            	        	}

            	        }
            	        break;

            	}

            	Match(input,30,FOLLOW_30_in_constvector818); if (state.failed) return result;
            	// Movimentum.g:68:9: ( '-' c= constscalar | c= constscalar )
            	int alt7 = 2;
            	int LA7_0 = input.LA(1);

            	if ( (LA7_0 == 28) )
            	{
            	    alt7 = 1;
            	}
            	else if ( (LA7_0 == NUMBER || LA7_0 == 29) )
            	{
            	    alt7 = 2;
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            	    NoViableAltException nvae_d7s0 =
            	        new NoViableAltException("", 7, 0, input);

            	    throw nvae_d7s0;
            	}
            	switch (alt7) 
            	{
            	    case 1 :
            	        // Movimentum.g:68:10: '-' c= constscalar
            	        {
            	        	Match(input,28,FOLLOW_28_in_constvector829); if (state.failed) return result;
            	        	PushFollow(FOLLOW_constscalar_in_constvector833);
            	        	c = constscalar();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return result;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   y = -c; 
            	        	}

            	        }
            	        break;
            	    case 2 :
            	        // Movimentum.g:69:14: c= constscalar
            	        {
            	        	PushFollow(FOLLOW_constscalar_in_constvector855);
            	        	c = constscalar();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return result;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   y = c; 
            	        	}

            	        }
            	        break;

            	}

            	// Movimentum.g:71:9: ( ',' ( '-' c= constscalar | c= constscalar ) | )
            	int alt9 = 2;
            	int LA9_0 = input.LA(1);

            	if ( (LA9_0 == 30) )
            	{
            	    alt9 = 1;
            	}
            	else if ( (LA9_0 == 31) )
            	{
            	    alt9 = 2;
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            	    NoViableAltException nvae_d9s0 =
            	        new NoViableAltException("", 9, 0, input);

            	    throw nvae_d9s0;
            	}
            	switch (alt9) 
            	{
            	    case 1 :
            	        // Movimentum.g:71:11: ',' ( '-' c= constscalar | c= constscalar )
            	        {
            	        	Match(input,30,FOLLOW_30_in_constvector882); if (state.failed) return result;
            	        	 double z = double.NaN; 
            	        	// Movimentum.g:72:11: ( '-' c= constscalar | c= constscalar )
            	        	int alt8 = 2;
            	        	int LA8_0 = input.LA(1);

            	        	if ( (LA8_0 == 28) )
            	        	{
            	        	    alt8 = 1;
            	        	}
            	        	else if ( (LA8_0 == NUMBER || LA8_0 == 29) )
            	        	{
            	        	    alt8 = 2;
            	        	}
            	        	else 
            	        	{
            	        	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            	        	    NoViableAltException nvae_d8s0 =
            	        	        new NoViableAltException("", 8, 0, input);

            	        	    throw nvae_d8s0;
            	        	}
            	        	switch (alt8) 
            	        	{
            	        	    case 1 :
            	        	        // Movimentum.g:72:12: '-' c= constscalar
            	        	        {
            	        	        	Match(input,28,FOLLOW_28_in_constvector913); if (state.failed) return result;
            	        	        	PushFollow(FOLLOW_constscalar_in_constvector917);
            	        	        	c = constscalar();
            	        	        	state.followingStackPointer--;
            	        	        	if (state.failed) return result;
            	        	        	if ( state.backtracking == 0 ) 
            	        	        	{
            	        	        	   z = -c; 
            	        	        	}

            	        	        }
            	        	        break;
            	        	    case 2 :
            	        	        // Movimentum.g:73:16: c= constscalar
            	        	        {
            	        	        	PushFollow(FOLLOW_constscalar_in_constvector939);
            	        	        	c = constscalar();
            	        	        	state.followingStackPointer--;
            	        	        	if (state.failed) return result;
            	        	        	if ( state.backtracking == 0 ) 
            	        	        	{
            	        	        	   z = c; 
            	        	        	}

            	        	        }
            	        	        break;

            	        	}

            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   result = new ConstVector(x,y,z); 
            	        	}

            	        }
            	        break;
            	    case 2 :
            	        // Movimentum.g:75:25: 
            	        {
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   result = new ConstVector(x,y); 
            	        	}

            	        }
            	        break;

            	}

            	Match(input,31,FOLLOW_31_in_constvector1014); if (state.failed) return result;

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "constvector"


    // $ANTLR start "constscalar"
    // Movimentum.g:80:5: constscalar returns [double result] : (n= number | c= constvector ( X | Y | Z ) );
    public double constscalar() // throws RecognitionException [1]
    {   
        double result = default(double);

        double n = default(double);

        ConstVector c = default(ConstVector);


        try 
    	{
            // Movimentum.g:81:7: (n= number | c= constvector ( X | Y | Z ) )
            int alt11 = 2;
            int LA11_0 = input.LA(1);

            if ( (LA11_0 == NUMBER) )
            {
                alt11 = 1;
            }
            else if ( (LA11_0 == 29) )
            {
                alt11 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d11s0 =
                    new NoViableAltException("", 11, 0, input);

                throw nvae_d11s0;
            }
            switch (alt11) 
            {
                case 1 :
                    // Movimentum.g:81:9: n= number
                    {
                    	PushFollow(FOLLOW_number_in_constscalar1062);
                    	n = number();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = n; 
                    	}

                    }
                    break;
                case 2 :
                    // Movimentum.g:82:9: c= constvector ( X | Y | Z )
                    {
                    	PushFollow(FOLLOW_constvector_in_constscalar1087);
                    	c = constvector();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	// Movimentum.g:83:9: ( X | Y | Z )
                    	int alt10 = 3;
                    	switch ( input.LA(1) ) 
                    	{
                    	case X:
                    		{
                    	    alt10 = 1;
                    	    }
                    	    break;
                    	case Y:
                    		{
                    	    alt10 = 2;
                    	    }
                    	    break;
                    	case Z:
                    		{
                    	    alt10 = 3;
                    	    }
                    	    break;
                    		default:
                    		    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    		    NoViableAltException nvae_d10s0 =
                    		        new NoViableAltException("", 10, 0, input);

                    		    throw nvae_d10s0;
                    	}

                    	switch (alt10) 
                    	{
                    	    case 1 :
                    	        // Movimentum.g:83:11: X
                    	        {
                    	        	Match(input,X,FOLLOW_X_in_constscalar1106); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = c.X; 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:84:11: Y
                    	        {
                    	        	Match(input,Y,FOLLOW_Y_in_constscalar1136); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = c.Y; 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:85:11: Z
                    	        {
                    	        	Match(input,Z,FOLLOW_Z_in_constscalar1166); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = c.Z; 
                    	        	}

                    	        }
                    	        break;

                    	}


                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "constscalar"


    // $ANTLR start "time"
    // Movimentum.g:89:5: time : ( '@' n= number | '@' '+' n= number );
    public void time() // throws RecognitionException [1]
    {   
        double n = default(double);


        try 
    	{
            // Movimentum.g:90:7: ( '@' n= number | '@' '+' n= number )
            int alt12 = 2;
            int LA12_0 = input.LA(1);

            if ( (LA12_0 == 32) )
            {
                int LA12_1 = input.LA(2);

                if ( (LA12_1 == 27) )
                {
                    alt12 = 2;
                }
                else if ( (LA12_1 == NUMBER) )
                {
                    alt12 = 1;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return ;}
                    NoViableAltException nvae_d12s1 =
                        new NoViableAltException("", 12, 1, input);

                    throw nvae_d12s1;
                }
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return ;}
                NoViableAltException nvae_d12s0 =
                    new NoViableAltException("", 12, 0, input);

                throw nvae_d12s0;
            }
            switch (alt12) 
            {
                case 1 :
                    // Movimentum.g:90:9: '@' n= number
                    {
                    	Match(input,32,FOLLOW_32_in_time1219); if (state.failed) return ;
                    	PushFollow(FOLLOW_number_in_time1223);
                    	n = number();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   _currentTime = n; 
                    	}

                    }
                    break;
                case 2 :
                    // Movimentum.g:91:9: '@' '+' n= number
                    {
                    	Match(input,32,FOLLOW_32_in_time1242); if (state.failed) return ;
                    	Match(input,27,FOLLOW_27_in_time1244); if (state.failed) return ;
                    	PushFollow(FOLLOW_number_in_time1248);
                    	n = number();
                    	state.followingStackPointer--;
                    	if (state.failed) return ;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   _currentTime += n; 
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "time"


    // $ANTLR start "constraint"
    // Movimentum.g:94:5: constraint returns [Constraint result] : (a= anchor '=' v= vectorexpr ';' | i= IDENT '=' s= scalarexpr ';' | i= IDENT ( '<' | '>' | '<=' | '>=' ) s= scalarexpr ';' );
    public Constraint constraint() // throws RecognitionException [1]
    {   
        Constraint result = default(Constraint);

        IToken i = null;
        Anchor a = default(Anchor);

        VectorExpr v = default(VectorExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:95:7: (a= anchor '=' v= vectorexpr ';' | i= IDENT '=' s= scalarexpr ';' | i= IDENT ( '<' | '>' | '<=' | '>=' ) s= scalarexpr ';' )
            int alt14 = 3;
            int LA14_0 = input.LA(1);

            if ( (LA14_0 == IDENT) )
            {
                switch ( input.LA(2) ) 
                {
                case 26:
                	{
                    alt14 = 2;
                    }
                    break;
                case 33:
                case 34:
                case 35:
                case 36:
                	{
                    alt14 = 3;
                    }
                    break;
                case 39:
                	{
                    alt14 = 1;
                    }
                    break;
                	default:
                	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                	    NoViableAltException nvae_d14s1 =
                	        new NoViableAltException("", 14, 1, input);

                	    throw nvae_d14s1;
                }

            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d14s0 =
                    new NoViableAltException("", 14, 0, input);

                throw nvae_d14s0;
            }
            switch (alt14) 
            {
                case 1 :
                    // Movimentum.g:95:9: a= anchor '=' v= vectorexpr ';'
                    {
                    	PushFollow(FOLLOW_anchor_in_constraint1284);
                    	a = anchor();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,26,FOLLOW_26_in_constraint1294); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_constraint1306);
                    	v = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,24,FOLLOW_24_in_constraint1316); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new VectorEqualityConstraint(a, v); 
                    	}

                    }
                    break;
                case 2 :
                    // Movimentum.g:99:9: i= IDENT '=' s= scalarexpr ';'
                    {
                    	i=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_constraint1346); if (state.failed) return result;
                    	Match(input,26,FOLLOW_26_in_constraint1356); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_constraint1368);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,24,FOLLOW_24_in_constraint1378); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ScalarEqualityConstraint(i.Text, s); 
                    	}

                    }
                    break;
                case 3 :
                    // Movimentum.g:103:9: i= IDENT ( '<' | '>' | '<=' | '>=' ) s= scalarexpr ';'
                    {
                    	i=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_constraint1408); if (state.failed) return result;
                    	 ScalarInequalityOperator op = null; 
                    	// Movimentum.g:104:9: ( '<' | '>' | '<=' | '>=' )
                    	int alt13 = 4;
                    	switch ( input.LA(1) ) 
                    	{
                    	case 33:
                    		{
                    	    alt13 = 1;
                    	    }
                    	    break;
                    	case 34:
                    		{
                    	    alt13 = 2;
                    	    }
                    	    break;
                    	case 35:
                    		{
                    	    alt13 = 3;
                    	    }
                    	    break;
                    	case 36:
                    		{
                    	    alt13 = 4;
                    	    }
                    	    break;
                    		default:
                    		    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    		    NoViableAltException nvae_d13s0 =
                    		        new NoViableAltException("", 13, 0, input);

                    		    throw nvae_d13s0;
                    	}

                    	switch (alt13) 
                    	{
                    	    case 1 :
                    	        // Movimentum.g:104:11: '<'
                    	        {
                    	        	Match(input,33,FOLLOW_33_in_constraint1434); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.LE; 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:105:11: '>'
                    	        {
                    	        	Match(input,34,FOLLOW_34_in_constraint1462); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.GE; 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:106:11: '<='
                    	        {
                    	        	Match(input,35,FOLLOW_35_in_constraint1490); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.LT; 
                    	        	}

                    	        }
                    	        break;
                    	    case 4 :
                    	        // Movimentum.g:107:11: '>='
                    	        {
                    	        	Match(input,36,FOLLOW_36_in_constraint1517); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.GT; 
                    	        	}

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_scalarexpr_in_constraint1573);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,24,FOLLOW_24_in_constraint1591); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ScalarInequalityConstraint(i.Text, op, s); 
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "constraint"


    // $ANTLR start "vectorexpr"
    // Movimentum.g:113:5: vectorexpr returns [VectorExpr result] : v= vectorexpr2 ( ( '+' | '-' ) v= vectorexpr2 )* ;
    public VectorExpr vectorexpr() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);


        try 
    	{
            // Movimentum.g:114:7: (v= vectorexpr2 ( ( '+' | '-' ) v= vectorexpr2 )* )
            // Movimentum.g:114:9: v= vectorexpr2 ( ( '+' | '-' ) v= vectorexpr2 )*
            {
            	PushFollow(FOLLOW_vectorexpr2_in_vectorexpr1640);
            	v = vectorexpr2();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = v; 
            	}
            	// Movimentum.g:115:9: ( ( '+' | '-' ) v= vectorexpr2 )*
            	do 
            	{
            	    int alt16 = 2;
            	    int LA16_0 = input.LA(1);

            	    if ( ((LA16_0 >= 27 && LA16_0 <= 28)) )
            	    {
            	        alt16 = 1;
            	    }


            	    switch (alt16) 
            		{
            			case 1 :
            			    // Movimentum.g:115:29: ( '+' | '-' ) v= vectorexpr2
            			    {
            			    	 BinaryVectorOperator op = null; 
            			    	// Movimentum.g:116:11: ( '+' | '-' )
            			    	int alt15 = 2;
            			    	int LA15_0 = input.LA(1);

            			    	if ( (LA15_0 == 27) )
            			    	{
            			    	    alt15 = 1;
            			    	}
            			    	else if ( (LA15_0 == 28) )
            			    	{
            			    	    alt15 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            			    	    NoViableAltException nvae_d15s0 =
            			    	        new NoViableAltException("", 15, 0, input);

            			    	    throw nvae_d15s0;
            			    	}
            			    	switch (alt15) 
            			    	{
            			    	    case 1 :
            			    	        // Movimentum.g:116:13: '+'
            			    	        {
            			    	        	Match(input,27,FOLLOW_27_in_vectorexpr1692); if (state.failed) return result;
            			    	        	 op = BinaryVectorOperator.PLUS; 

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // Movimentum.g:117:13: '-'
            			    	        {
            			    	        	Match(input,28,FOLLOW_28_in_vectorexpr1720); if (state.failed) return result;
            			    	        	 op = BinaryVectorOperator.MINUS; 

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_vectorexpr2_in_vectorexpr1777);
            			    	v = vectorexpr2();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return result;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	   result = new BinaryVectorExpr(result, op, v); 
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop16;
            	    }
            	} while (true);

            	loop16:
            		;	// Stops C# compiler whining that label 'loop16' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "vectorexpr"


    // $ANTLR start "vectorexpr2"
    // Movimentum.g:123:5: vectorexpr2 returns [VectorExpr result] : v= vectorexpr3 ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )* ;
    public VectorExpr vectorexpr2() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:124:7: (v= vectorexpr3 ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )* )
            // Movimentum.g:124:9: v= vectorexpr3 ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )*
            {
            	PushFollow(FOLLOW_vectorexpr3_in_vectorexpr21892);
            	v = vectorexpr3();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = v; 
            	}
            	// Movimentum.g:125:9: ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )*
            	do 
            	{
            	    int alt17 = 3;
            	    int LA17_0 = input.LA(1);

            	    if ( (LA17_0 == ROTATE) )
            	    {
            	        alt17 = 1;
            	    }
            	    else if ( (LA17_0 == 37) )
            	    {
            	        alt17 = 2;
            	    }


            	    switch (alt17) 
            		{
            			case 1 :
            			    // Movimentum.g:125:11: ROTATE '(' s= scalarexpr ')'
            			    {
            			    	Match(input,ROTATE,FOLLOW_ROTATE_in_vectorexpr21912); if (state.failed) return result;
            			    	Match(input,22,FOLLOW_22_in_vectorexpr21924); if (state.failed) return result;
            			    	PushFollow(FOLLOW_scalarexpr_in_vectorexpr21938);
            			    	s = scalarexpr();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return result;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	   result = new VectorScalarExpr(result, VectorScalarOperator.ROTATE, s); 
            			    	}
            			    	Match(input,23,FOLLOW_23_in_vectorexpr21957); if (state.failed) return result;

            			    }
            			    break;
            			case 2 :
            			    // Movimentum.g:129:5: '*' s= scalarexpr4
            			    {
            			    	Match(input,37,FOLLOW_37_in_vectorexpr21963); if (state.failed) return result;
            			    	PushFollow(FOLLOW_scalarexpr4_in_vectorexpr21971);
            			    	s = scalarexpr4();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return result;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	   result = new VectorScalarExpr(result, VectorScalarOperator.TIMES, s); 
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop17;
            	    }
            	} while (true);

            	loop17:
            		;	// Stops C# compiler whining that label 'loop17' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "vectorexpr2"


    // $ANTLR start "vectorexpr3"
    // Movimentum.g:134:5: vectorexpr3 returns [VectorExpr result] : ( '-' v= vectorexpr4 | v= vectorexpr4 );
    public VectorExpr vectorexpr3() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);


        try 
    	{
            // Movimentum.g:135:7: ( '-' v= vectorexpr4 | v= vectorexpr4 )
            int alt18 = 2;
            int LA18_0 = input.LA(1);

            if ( (LA18_0 == 28) )
            {
                alt18 = 1;
            }
            else if ( (LA18_0 == IDENT || (LA18_0 >= INTEGRAL && LA18_0 <= DIFFERENTIAL) || LA18_0 == 22 || LA18_0 == 29 || LA18_0 == 38) )
            {
                alt18 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d18s0 =
                    new NoViableAltException("", 18, 0, input);

                throw nvae_d18s0;
            }
            switch (alt18) 
            {
                case 1 :
                    // Movimentum.g:135:9: '-' v= vectorexpr4
                    {
                    	Match(input,28,FOLLOW_28_in_vectorexpr32017); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr4_in_vectorexpr32021);
                    	v = vectorexpr4();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryVectorExpr(UnaryVectorOperator.MINUS, v); 
                    	}

                    }
                    break;
                case 2 :
                    // Movimentum.g:136:9: v= vectorexpr4
                    {
                    	PushFollow(FOLLOW_vectorexpr4_in_vectorexpr32037);
                    	v = vectorexpr4();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = v; 
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "vectorexpr3"


    // $ANTLR start "vectorexpr4"
    // Movimentum.g:139:5: vectorexpr4 returns [VectorExpr result] : ( INTEGRAL '(' v= vectorexpr ')' | DIFFERENTIAL '(' v= vectorexpr ')' | v= vectorexpr5 );
    public VectorExpr vectorexpr4() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);


        try 
    	{
            // Movimentum.g:140:7: ( INTEGRAL '(' v= vectorexpr ')' | DIFFERENTIAL '(' v= vectorexpr ')' | v= vectorexpr5 )
            int alt19 = 3;
            switch ( input.LA(1) ) 
            {
            case INTEGRAL:
            	{
                alt19 = 1;
                }
                break;
            case DIFFERENTIAL:
            	{
                alt19 = 2;
                }
                break;
            case IDENT:
            case 22:
            case 29:
            case 38:
            	{
                alt19 = 3;
                }
                break;
            	default:
            	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            	    NoViableAltException nvae_d19s0 =
            	        new NoViableAltException("", 19, 0, input);

            	    throw nvae_d19s0;
            }

            switch (alt19) 
            {
                case 1 :
                    // Movimentum.g:140:9: INTEGRAL '(' v= vectorexpr ')'
                    {
                    	Match(input,INTEGRAL,FOLLOW_INTEGRAL_in_vectorexpr42074); if (state.failed) return result;
                    	Match(input,22,FOLLOW_22_in_vectorexpr42084); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_vectorexpr42096);
                    	v = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryVectorExpr(UnaryVectorOperator.INTEGRAL, v); 
                    	}
                    	Match(input,23,FOLLOW_23_in_vectorexpr42115); if (state.failed) return result;

                    }
                    break;
                case 2 :
                    // Movimentum.g:144:9: DIFFERENTIAL '(' v= vectorexpr ')'
                    {
                    	Match(input,DIFFERENTIAL,FOLLOW_DIFFERENTIAL_in_vectorexpr42142); if (state.failed) return result;
                    	Match(input,22,FOLLOW_22_in_vectorexpr42160); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_vectorexpr42189);
                    	v = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryVectorExpr(UnaryVectorOperator.DIFFERENTIAL, v); 
                    	}
                    	Match(input,23,FOLLOW_23_in_vectorexpr42208); if (state.failed) return result;

                    }
                    break;
                case 3 :
                    // Movimentum.g:148:9: v= vectorexpr5
                    {
                    	PushFollow(FOLLOW_vectorexpr5_in_vectorexpr42237);
                    	v = vectorexpr5();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = v; 
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "vectorexpr4"


    // $ANTLR start "vectorexpr5"
    // Movimentum.g:151:5: vectorexpr5 returns [VectorExpr result] : ( '(' e= vectorexpr ')' | v= vector | a= anchor | IDENT | '_' );
    public VectorExpr vectorexpr5() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        IToken IDENT3 = null;
        VectorExpr e = default(VectorExpr);

        Vector v = default(Vector);

        Anchor a = default(Anchor);


        try 
    	{
            // Movimentum.g:152:7: ( '(' e= vectorexpr ')' | v= vector | a= anchor | IDENT | '_' )
            int alt20 = 5;
            switch ( input.LA(1) ) 
            {
            case 22:
            	{
                alt20 = 1;
                }
                break;
            case 29:
            	{
                alt20 = 2;
                }
                break;
            case IDENT:
            	{
                int LA20_3 = input.LA(2);

                if ( ((LA20_3 >= X && LA20_3 <= ROTATE) || LA20_3 == LENGTH || (LA20_3 >= 23 && LA20_3 <= 24) || (LA20_3 >= 27 && LA20_3 <= 28) || LA20_3 == 30 || LA20_3 == 37) )
                {
                    alt20 = 4;
                }
                else if ( (LA20_3 == 39) )
                {
                    alt20 = 3;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    NoViableAltException nvae_d20s3 =
                        new NoViableAltException("", 20, 3, input);

                    throw nvae_d20s3;
                }
                }
                break;
            case 38:
            	{
                alt20 = 5;
                }
                break;
            	default:
            	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            	    NoViableAltException nvae_d20s0 =
            	        new NoViableAltException("", 20, 0, input);

            	    throw nvae_d20s0;
            }

            switch (alt20) 
            {
                case 1 :
                    // Movimentum.g:152:9: '(' e= vectorexpr ')'
                    {
                    	Match(input,22,FOLLOW_22_in_vectorexpr52274); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_vectorexpr52286);
                    	e = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = e; 
                    	}
                    	Match(input,23,FOLLOW_23_in_vectorexpr52305); if (state.failed) return result;

                    }
                    break;
                case 2 :
                    // Movimentum.g:155:9: v= vector
                    {
                    	PushFollow(FOLLOW_vector_in_vectorexpr52334);
                    	v = vector();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = v; 
                    	}

                    }
                    break;
                case 3 :
                    // Movimentum.g:156:9: a= anchor
                    {
                    	PushFollow(FOLLOW_anchor_in_vectorexpr52359);
                    	a = anchor();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = a; 
                    	}

                    }
                    break;
                case 4 :
                    // Movimentum.g:157:9: IDENT
                    {
                    	IDENT3=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_vectorexpr52382); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new VectorVariable(IDENT3.Text); 
                    	}

                    }
                    break;
                case 5 :
                    // Movimentum.g:158:9: '_'
                    {
                    	Match(input,38,FOLLOW_38_in_vectorexpr52408); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new VectorVariable("#" + _anonymousVarCt++); 
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "vectorexpr5"


    // $ANTLR start "vector"
    // Movimentum.g:161:5: vector returns [Vector result] : '[' s1= scalarexpr ',' s2= scalarexpr ( ',' s3= scalarexpr )? ']' ;
    public Vector vector() // throws RecognitionException [1]
    {   
        Vector result = default(Vector);

        ScalarExpr s1 = default(ScalarExpr);

        ScalarExpr s2 = default(ScalarExpr);

        ScalarExpr s3 = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:162:7: ( '[' s1= scalarexpr ',' s2= scalarexpr ( ',' s3= scalarexpr )? ']' )
            // Movimentum.g:162:9: '[' s1= scalarexpr ',' s2= scalarexpr ( ',' s3= scalarexpr )? ']'
            {
            	Match(input,29,FOLLOW_29_in_vector2504); if (state.failed) return result;
            	PushFollow(FOLLOW_scalarexpr_in_vector2516);
            	s1 = scalarexpr();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,30,FOLLOW_30_in_vector2526); if (state.failed) return result;
            	PushFollow(FOLLOW_scalarexpr_in_vector2538);
            	s2 = scalarexpr();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	// Movimentum.g:166:9: ( ',' s3= scalarexpr )?
            	int alt21 = 2;
            	int LA21_0 = input.LA(1);

            	if ( (LA21_0 == 30) )
            	{
            	    alt21 = 1;
            	}
            	switch (alt21) 
            	{
            	    case 1 :
            	        // Movimentum.g:166:11: ',' s3= scalarexpr
            	        {
            	        	Match(input,30,FOLLOW_30_in_vector2550); if (state.failed) return result;
            	        	PushFollow(FOLLOW_scalarexpr_in_vector2564);
            	        	s3 = scalarexpr();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return result;

            	        }
            	        break;

            	}

            	Match(input,31,FOLLOW_31_in_vector2579); if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = new Vector(s1, s2, s3 ?? new Constant(0)); 
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "vector"


    // $ANTLR start "anchor"
    // Movimentum.g:172:5: anchor returns [Anchor result] : th= thing '.' IDENT ;
    public Anchor anchor() // throws RecognitionException [1]
    {   
        Anchor result = default(Anchor);

        IToken IDENT4 = null;
        string th = default(string);


        try 
    	{
            // Movimentum.g:173:7: (th= thing '.' IDENT )
            // Movimentum.g:173:10: th= thing '.' IDENT
            {
            	PushFollow(FOLLOW_thing_in_anchor2629);
            	th = thing();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,39,FOLLOW_39_in_anchor2641); if (state.failed) return result;
            	IDENT4=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_anchor2653); if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = new Anchor(th, IDENT4.Text); 
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "anchor"


    // $ANTLR start "thing"
    // Movimentum.g:178:5: thing returns [string result] : IDENT ;
    public string thing() // throws RecognitionException [1]
    {   
        string result = default(string);

        IToken IDENT5 = null;

        try 
    	{
            // Movimentum.g:179:7: ( IDENT )
            // Movimentum.g:179:9: IDENT
            {
            	IDENT5=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_thing2697); if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = IDENT5.Text; 
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "thing"


    // $ANTLR start "scalarexpr"
    // Movimentum.g:182:5: scalarexpr returns [ScalarExpr result] : s= scalarexpr2 ( ( '+' | '-' ) s= scalarexpr2 )* ;
    public ScalarExpr scalarexpr() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:183:7: (s= scalarexpr2 ( ( '+' | '-' ) s= scalarexpr2 )* )
            // Movimentum.g:183:9: s= scalarexpr2 ( ( '+' | '-' ) s= scalarexpr2 )*
            {
            	PushFollow(FOLLOW_scalarexpr2_in_scalarexpr2744);
            	s = scalarexpr2();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = s; 
            	}
            	// Movimentum.g:184:9: ( ( '+' | '-' ) s= scalarexpr2 )*
            	do 
            	{
            	    int alt23 = 2;
            	    int LA23_0 = input.LA(1);

            	    if ( ((LA23_0 >= 27 && LA23_0 <= 28)) )
            	    {
            	        alt23 = 1;
            	    }


            	    switch (alt23) 
            		{
            			case 1 :
            			    // Movimentum.g:184:29: ( '+' | '-' ) s= scalarexpr2
            			    {
            			    	 BinaryScalarOperator op = null; 
            			    	// Movimentum.g:185:11: ( '+' | '-' )
            			    	int alt22 = 2;
            			    	int LA22_0 = input.LA(1);

            			    	if ( (LA22_0 == 27) )
            			    	{
            			    	    alt22 = 1;
            			    	}
            			    	else if ( (LA22_0 == 28) )
            			    	{
            			    	    alt22 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            			    	    NoViableAltException nvae_d22s0 =
            			    	        new NoViableAltException("", 22, 0, input);

            			    	    throw nvae_d22s0;
            			    	}
            			    	switch (alt22) 
            			    	{
            			    	    case 1 :
            			    	        // Movimentum.g:185:13: '+'
            			    	        {
            			    	        	Match(input,27,FOLLOW_27_in_scalarexpr2796); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.PLUS; 

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // Movimentum.g:186:13: '-'
            			    	        {
            			    	        	Match(input,28,FOLLOW_28_in_scalarexpr2824); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.MINUS; 

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_scalarexpr2_in_scalarexpr2881);
            			    	s = scalarexpr2();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return result;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	   result = new BinaryScalarExpr(result, op, s); 
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop23;
            	    }
            	} while (true);

            	loop23:
            		;	// Stops C# compiler whining that label 'loop23' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "scalarexpr"


    // $ANTLR start "scalarexpr2"
    // Movimentum.g:192:5: scalarexpr2 returns [ScalarExpr result] : s= scalarexpr3 ( ( '*' | '/' ) s= scalarexpr3 )* ;
    public ScalarExpr scalarexpr2() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:193:7: (s= scalarexpr3 ( ( '*' | '/' ) s= scalarexpr3 )* )
            // Movimentum.g:193:9: s= scalarexpr3 ( ( '*' | '/' ) s= scalarexpr3 )*
            {
            	PushFollow(FOLLOW_scalarexpr3_in_scalarexpr22929);
            	s = scalarexpr3();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = s; 
            	}
            	// Movimentum.g:194:9: ( ( '*' | '/' ) s= scalarexpr3 )*
            	do 
            	{
            	    int alt25 = 2;
            	    int LA25_0 = input.LA(1);

            	    if ( (LA25_0 == 37 || LA25_0 == 40) )
            	    {
            	        alt25 = 1;
            	    }


            	    switch (alt25) 
            		{
            			case 1 :
            			    // Movimentum.g:194:29: ( '*' | '/' ) s= scalarexpr3
            			    {
            			    	 BinaryScalarOperator op = null; 
            			    	// Movimentum.g:195:11: ( '*' | '/' )
            			    	int alt24 = 2;
            			    	int LA24_0 = input.LA(1);

            			    	if ( (LA24_0 == 37) )
            			    	{
            			    	    alt24 = 1;
            			    	}
            			    	else if ( (LA24_0 == 40) )
            			    	{
            			    	    alt24 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            			    	    NoViableAltException nvae_d24s0 =
            			    	        new NoViableAltException("", 24, 0, input);

            			    	    throw nvae_d24s0;
            			    	}
            			    	switch (alt24) 
            			    	{
            			    	    case 1 :
            			    	        // Movimentum.g:195:13: '*'
            			    	        {
            			    	        	Match(input,37,FOLLOW_37_in_scalarexpr22981); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.TIMES; 

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // Movimentum.g:196:13: '/'
            			    	        {
            			    	        	Match(input,40,FOLLOW_40_in_scalarexpr23009); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.DIVIDE; 

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_scalarexpr3_in_scalarexpr23066);
            			    	s = scalarexpr3();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return result;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	   result = new BinaryScalarExpr(result, op, s); 
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop25;
            	    }
            	} while (true);

            	loop25:
            		;	// Stops C# compiler whining that label 'loop25' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "scalarexpr2"


    // $ANTLR start "scalarexpr3"
    // Movimentum.g:202:5: scalarexpr3 returns [ScalarExpr result] : ( '-' s= scalarexpr4 | s= scalarexpr4 );
    public ScalarExpr scalarexpr3() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:203:7: ( '-' s= scalarexpr4 | s= scalarexpr4 )
            int alt26 = 2;
            int LA26_0 = input.LA(1);

            if ( (LA26_0 == 28) )
            {
                alt26 = 1;
            }
            else if ( (LA26_0 == IDENT || (LA26_0 >= INTEGRAL && LA26_0 <= IV) || LA26_0 == NUMBER || LA26_0 == 22 || LA26_0 == 29 || LA26_0 == 38) )
            {
                alt26 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d26s0 =
                    new NoViableAltException("", 26, 0, input);

                throw nvae_d26s0;
            }
            switch (alt26) 
            {
                case 1 :
                    // Movimentum.g:203:9: '-' s= scalarexpr4
                    {
                    	Match(input,28,FOLLOW_28_in_scalarexpr33151); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr4_in_scalarexpr33155);
                    	s = scalarexpr4();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryScalarExpr(UnaryScalarOperator.MINUS, s); 
                    	}

                    }
                    break;
                case 2 :
                    // Movimentum.g:204:9: s= scalarexpr4
                    {
                    	PushFollow(FOLLOW_scalarexpr4_in_scalarexpr33171);
                    	s = scalarexpr4();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = s; 
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "scalarexpr3"


    // $ANTLR start "scalarexpr4"
    // Movimentum.g:207:5: scalarexpr4 returns [ScalarExpr result] : (s= scalarexpr5 ( '^2' | '^3' | ) | SQRT '(' s= scalarexpr5 ')' );
    public ScalarExpr scalarexpr4() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:208:4: (s= scalarexpr5 ( '^2' | '^3' | ) | SQRT '(' s= scalarexpr5 ')' )
            int alt28 = 2;
            int LA28_0 = input.LA(1);

            if ( (LA28_0 == IDENT || (LA28_0 >= INTEGRAL && LA28_0 <= DIFFERENTIAL) || (LA28_0 >= ANGLE && LA28_0 <= IV) || LA28_0 == NUMBER || LA28_0 == 22 || LA28_0 == 29 || LA28_0 == 38) )
            {
                alt28 = 1;
            }
            else if ( (LA28_0 == SQRT) )
            {
                alt28 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d28s0 =
                    new NoViableAltException("", 28, 0, input);

                throw nvae_d28s0;
            }
            switch (alt28) 
            {
                case 1 :
                    // Movimentum.g:208:6: s= scalarexpr5 ( '^2' | '^3' | )
                    {
                    	PushFollow(FOLLOW_scalarexpr5_in_scalarexpr43207);
                    	s = scalarexpr5();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	// Movimentum.g:209:6: ( '^2' | '^3' | )
                    	int alt27 = 3;
                    	switch ( input.LA(1) ) 
                    	{
                    	case 41:
                    		{
                    	    alt27 = 1;
                    	    }
                    	    break;
                    	case 42:
                    		{
                    	    alt27 = 2;
                    	    }
                    	    break;
                    	case ROTATE:
                    	case 23:
                    	case 24:
                    	case 27:
                    	case 28:
                    	case 30:
                    	case 31:
                    	case 37:
                    	case 40:
                    		{
                    	    alt27 = 3;
                    	    }
                    	    break;
                    		default:
                    		    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    		    NoViableAltException nvae_d27s0 =
                    		        new NoViableAltException("", 27, 0, input);

                    		    throw nvae_d27s0;
                    	}

                    	switch (alt27) 
                    	{
                    	    case 1 :
                    	        // Movimentum.g:209:8: '^2'
                    	        {
                    	        	Match(input,41,FOLLOW_41_in_scalarexpr43216); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarExpr(UnaryScalarOperator.SQUARED, s); 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:210:5: '^3'
                    	        {
                    	        	Match(input,42,FOLLOW_42_in_scalarexpr43237); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarExpr(UnaryScalarOperator.CUBED, s); 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:211:23: 
                    	        {
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = s; 
                    	        	}

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // Movimentum.g:213:6: SQRT '(' s= scalarexpr5 ')'
                    {
                    	Match(input,SQRT,FOLLOW_SQRT_in_scalarexpr43287); if (state.failed) return result;
                    	Match(input,22,FOLLOW_22_in_scalarexpr43294); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr5_in_scalarexpr43306);
                    	s = scalarexpr5();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryScalarExpr(UnaryScalarOperator.SQUAREROOT, s); 
                    	}
                    	Match(input,23,FOLLOW_23_in_scalarexpr43321); if (state.failed) return result;

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "scalarexpr4"


    // $ANTLR start "scalarexpr5"
    // Movimentum.g:219:5: scalarexpr5 returns [ScalarExpr result] : (s= scalarexpr5Ambiguous | INTEGRAL '(' s= scalarexpr ')' | DIFFERENTIAL '(' s= scalarexpr ')' | ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')' | n= number | IDENT | '_' | T | IV );
    public ScalarExpr scalarexpr5() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        IToken IDENT6 = null;
        ScalarExpr s = default(ScalarExpr);

        VectorExpr v1 = default(VectorExpr);

        VectorExpr v2 = default(VectorExpr);

        double n = default(double);


        try 
    	{
            // Movimentum.g:220:7: (s= scalarexpr5Ambiguous | INTEGRAL '(' s= scalarexpr ')' | DIFFERENTIAL '(' s= scalarexpr ')' | ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')' | n= number | IDENT | '_' | T | IV )
            int alt29 = 9;
            alt29 = dfa29.Predict(input);
            switch (alt29) 
            {
                case 1 :
                    // Movimentum.g:220:9: s= scalarexpr5Ambiguous
                    {
                    	PushFollow(FOLLOW_scalarexpr5Ambiguous_in_scalarexpr53349);
                    	s = scalarexpr5Ambiguous();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = s; 
                    	}

                    }
                    break;
                case 2 :
                    // Movimentum.g:222:9: INTEGRAL '(' s= scalarexpr ')'
                    {
                    	Match(input,INTEGRAL,FOLLOW_INTEGRAL_in_scalarexpr53389); if (state.failed) return result;
                    	Match(input,22,FOLLOW_22_in_scalarexpr53391); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr53413);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_scalarexpr53431); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryScalarExpr(UnaryScalarOperator.INTEGRAL, s); 
                    	}

                    }
                    break;
                case 3 :
                    // Movimentum.g:225:9: DIFFERENTIAL '(' s= scalarexpr ')'
                    {
                    	Match(input,DIFFERENTIAL,FOLLOW_DIFFERENTIAL_in_scalarexpr53457); if (state.failed) return result;
                    	Match(input,22,FOLLOW_22_in_scalarexpr53459); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr53477);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_scalarexpr53495); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryScalarExpr(UnaryScalarOperator.DIFFERENTIAL, s); 
                    	}

                    }
                    break;
                case 4 :
                    // Movimentum.g:228:9: ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')'
                    {
                    	Match(input,ANGLE,FOLLOW_ANGLE_in_scalarexpr53521); if (state.failed) return result;
                    	Match(input,22,FOLLOW_22_in_scalarexpr53523); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_scalarexpr53548);
                    	v1 = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,30,FOLLOW_30_in_scalarexpr53565); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_scalarexpr53594);
                    	v2 = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_scalarexpr53606); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new BinaryScalarVectorExpr(v1, BinaryScalarVectorOperator.ANGLE, v2); 
                    	}

                    }
                    break;
                case 5 :
                    // Movimentum.g:233:9: n= number
                    {
                    	PushFollow(FOLLOW_number_in_scalarexpr53634);
                    	n = number();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new Constant(n); 
                    	}

                    }
                    break;
                case 6 :
                    // Movimentum.g:234:9: IDENT
                    {
                    	IDENT6=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_scalarexpr53657); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ScalarVariable(IDENT6.Text); 
                    	}

                    }
                    break;
                case 7 :
                    // Movimentum.g:235:9: '_'
                    {
                    	Match(input,38,FOLLOW_38_in_scalarexpr53683); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ScalarVariable("_" + _anonymousVarCt++); 
                    	}

                    }
                    break;
                case 8 :
                    // Movimentum.g:237:9: T
                    {
                    	Match(input,T,FOLLOW_T_in_scalarexpr53718); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new T(); 
                    	}

                    }
                    break;
                case 9 :
                    // Movimentum.g:238:9: IV
                    {
                    	Match(input,IV,FOLLOW_IV_in_scalarexpr53748); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new IV(); 
                    	}

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "scalarexpr5"


    // $ANTLR start "scalarexpr5Ambiguous"
    // Movimentum.g:241:4: scalarexpr5Ambiguous returns [ScalarExpr result] options {backtrack=true; } : (v= vectorexpr5 ( X | Y | Z | LENGTH ) | '(' s= scalarexpr ')' );
    public ScalarExpr scalarexpr5Ambiguous() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        VectorExpr v = default(VectorExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:243:6: (v= vectorexpr5 ( X | Y | Z | LENGTH ) | '(' s= scalarexpr ')' )
            int alt31 = 2;
            int LA31_0 = input.LA(1);

            if ( (LA31_0 == 22) )
            {
                int LA31_1 = input.LA(2);

                if ( (synpred1_Movimentum()) )
                {
                    alt31 = 1;
                }
                else if ( (true) )
                {
                    alt31 = 2;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    NoViableAltException nvae_d31s1 =
                        new NoViableAltException("", 31, 1, input);

                    throw nvae_d31s1;
                }
            }
            else if ( (LA31_0 == IDENT || LA31_0 == 29 || LA31_0 == 38) )
            {
                alt31 = 1;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d31s0 =
                    new NoViableAltException("", 31, 0, input);

                throw nvae_d31s0;
            }
            switch (alt31) 
            {
                case 1 :
                    // Movimentum.g:243:9: v= vectorexpr5 ( X | Y | Z | LENGTH )
                    {
                    	PushFollow(FOLLOW_vectorexpr5_in_scalarexpr5Ambiguous3849);
                    	v = vectorexpr5();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	// Movimentum.g:244:8: ( X | Y | Z | LENGTH )
                    	int alt30 = 4;
                    	switch ( input.LA(1) ) 
                    	{
                    	case X:
                    		{
                    	    alt30 = 1;
                    	    }
                    	    break;
                    	case Y:
                    		{
                    	    alt30 = 2;
                    	    }
                    	    break;
                    	case Z:
                    		{
                    	    alt30 = 3;
                    	    }
                    	    break;
                    	case LENGTH:
                    		{
                    	    alt30 = 4;
                    	    }
                    	    break;
                    		default:
                    		    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    		    NoViableAltException nvae_d30s0 =
                    		        new NoViableAltException("", 30, 0, input);

                    		    throw nvae_d30s0;
                    	}

                    	switch (alt30) 
                    	{
                    	    case 1 :
                    	        // Movimentum.g:244:10: X
                    	        {
                    	        	Match(input,X,FOLLOW_X_in_scalarexpr5Ambiguous3860); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.X); 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:245:10: Y
                    	        {
                    	        	Match(input,Y,FOLLOW_Y_in_scalarexpr5Ambiguous3890); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.Y); 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:246:10: Z
                    	        {
                    	        	Match(input,Z,FOLLOW_Z_in_scalarexpr5Ambiguous3920); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.Z); 
                    	        	}

                    	        }
                    	        break;
                    	    case 4 :
                    	        // Movimentum.g:247:10: LENGTH
                    	        {
                    	        	Match(input,LENGTH,FOLLOW_LENGTH_in_scalarexpr5Ambiguous3950); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.LENGTH); 
                    	        	}

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // Movimentum.g:249:8: '(' s= scalarexpr ')'
                    {
                    	Match(input,22,FOLLOW_22_in_scalarexpr5Ambiguous3998); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr5Ambiguous4023);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = s; 
                    	}
                    	Match(input,23,FOLLOW_23_in_scalarexpr5Ambiguous4042); if (state.failed) return result;

                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return result;
    }
    // $ANTLR end "scalarexpr5Ambiguous"


    // $ANTLR start "number"
    // Movimentum.g:254:4: number returns [double value] : NUMBER ;
    public double number() // throws RecognitionException [1]
    {   
        double value = default(double);

        IToken NUMBER7 = null;

        try 
    	{
            // Movimentum.g:255:6: ( NUMBER )
            // Movimentum.g:255:8: NUMBER
            {
            	NUMBER7=(IToken)Match(input,NUMBER,FOLLOW_NUMBER_in_number4068); if (state.failed) return value;
            	if ( state.backtracking == 0 ) 
            	{
            	   value = double.Parse(NUMBER7.Text, CultureInfo.InvariantCulture); 
            	}

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return value;
    }
    // $ANTLR end "number"

    // $ANTLR start "synpred1_Movimentum"
    public void synpred1_Movimentum_fragment() {
        VectorExpr v = default(VectorExpr);


        // Movimentum.g:243:9: (v= vectorexpr5 ( X | Y | Z | LENGTH ) )
        // Movimentum.g:243:9: v= vectorexpr5 ( X | Y | Z | LENGTH )
        {
        	PushFollow(FOLLOW_vectorexpr5_in_synpred1_Movimentum3849);
        	v = vectorexpr5();
        	state.followingStackPointer--;
        	if (state.failed) return ;
        	if ( (input.LA(1) >= X && input.LA(1) <= Z) || input.LA(1) == LENGTH ) 
        	{
        	    input.Consume();
        	    state.errorRecovery = false;state.failed = false;
        	}
        	else 
        	{
        	    if ( state.backtracking > 0 ) {state.failed = true; return ;}
        	    MismatchedSetException mse = new MismatchedSetException(null,input);
        	    throw mse;
        	}


        }
    }
    // $ANTLR end "synpred1_Movimentum"

    // Delegated rules

   	public bool synpred1_Movimentum() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred1_Movimentum_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}


   	protected DFA29 dfa29;
	private void InitializeCyclicDFAs()
	{
    	this.dfa29 = new DFA29(this);
	}

    const string DFA29_eotS =
        "\x0c\uffff";
    const string DFA29_eofS =
        "\x0c\uffff";
    const string DFA29_minS =
        "\x01\x05\x01\uffff\x02\x07\x08\uffff";
    const string DFA29_maxS =
        "\x01\x26\x01\uffff\x02\x2a\x08\uffff";
    const string DFA29_acceptS =
        "\x01\uffff\x01\x01\x02\uffff\x01\x02\x01\x03\x01\x04\x01\x05\x01"+
        "\x08\x01\x09\x01\x06\x01\x07";
    const string DFA29_specialS =
        "\x0c\uffff}>";
    static readonly string[] DFA29_transitionS = {
            "\x01\x02\x05\uffff\x01\x04\x01\x05\x01\uffff\x01\x06\x01\x08"+
            "\x01\x09\x01\uffff\x01\x07\x03\uffff\x01\x01\x06\uffff\x01\x01"+
            "\x08\uffff\x01\x03",
            "",
            "\x03\x01\x01\x0a\x06\uffff\x01\x01\x05\uffff\x02\x0a\x02\uffff"+
            "\x02\x0a\x01\uffff\x02\x0a\x05\uffff\x01\x0a\x01\uffff\x01\x01"+
            "\x03\x0a",
            "\x03\x01\x01\x0b\x06\uffff\x01\x01\x05\uffff\x02\x0b\x02\uffff"+
            "\x02\x0b\x01\uffff\x02\x0b\x05\uffff\x01\x0b\x02\uffff\x03\x0b",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA29_eot = DFA.UnpackEncodedString(DFA29_eotS);
    static readonly short[] DFA29_eof = DFA.UnpackEncodedString(DFA29_eofS);
    static readonly char[] DFA29_min = DFA.UnpackEncodedStringToUnsignedChars(DFA29_minS);
    static readonly char[] DFA29_max = DFA.UnpackEncodedStringToUnsignedChars(DFA29_maxS);
    static readonly short[] DFA29_accept = DFA.UnpackEncodedString(DFA29_acceptS);
    static readonly short[] DFA29_special = DFA.UnpackEncodedString(DFA29_specialS);
    static readonly short[][] DFA29_transition = DFA.UnpackEncodedStringArray(DFA29_transitionS);

    protected class DFA29 : DFA
    {
        public DFA29(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 29;
            this.eot = DFA29_eot;
            this.eof = DFA29_eof;
            this.min = DFA29_min;
            this.max = DFA29_max;
            this.accept = DFA29_accept;
            this.special = DFA29_special;
            this.transition = DFA29_transition;

        }

        override public string Description
        {
            get { return "219:5: scalarexpr5 returns [ScalarExpr result] : (s= scalarexpr5Ambiguous | INTEGRAL '(' s= scalarexpr ')' | DIFFERENTIAL '(' s= scalarexpr ')' | ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')' | n= number | IDENT | '_' | T | IV );"; }
        }

    }

 

    public static readonly BitSet FOLLOW_config_in_script124 = new BitSet(new ulong[]{0x0000000100000020UL});
    public static readonly BitSet FOLLOW_thingdefinition_in_script138 = new BitSet(new ulong[]{0x0000000100000020UL});
    public static readonly BitSet FOLLOW_time_in_script163 = new BitSet(new ulong[]{0x0000000100000020UL});
    public static readonly BitSet FOLLOW_constraint_in_script194 = new BitSet(new ulong[]{0x0000000100000020UL});
    public static readonly BitSet FOLLOW_EOF_in_script269 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CONFIG_in_config298 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_config300 = new BitSet(new ulong[]{0x0000000000040000UL});
    public static readonly BitSet FOLLOW_number_in_config318 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_config330 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_24_in_config358 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_thingdefinition387 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_thingdefinition397 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_source_in_thingdefinition409 = new BitSet(new ulong[]{0x0000000000000020UL});
    public static readonly BitSet FOLLOW_anchordefinition_in_thingdefinition433 = new BitSet(new ulong[]{0x0000000001000020UL});
    public static readonly BitSet FOLLOW_24_in_thingdefinition455 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_FILENAME_in_source502 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchordefinition544 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_26_in_anchordefinition554 = new BitSet(new ulong[]{0x0000000020000020UL});
    public static readonly BitSet FOLLOW_constvector_in_anchordefinition595 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchordefinition624 = new BitSet(new ulong[]{0x0000000008000000UL});
    public static readonly BitSet FOLLOW_27_in_anchordefinition626 = new BitSet(new ulong[]{0x0000000020000000UL});
    public static readonly BitSet FOLLOW_constvector_in_anchordefinition630 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchordefinition647 = new BitSet(new ulong[]{0x0000000010000000UL});
    public static readonly BitSet FOLLOW_28_in_anchordefinition649 = new BitSet(new ulong[]{0x0000000020000000UL});
    public static readonly BitSet FOLLOW_constvector_in_anchordefinition653 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_29_in_constvector756 = new BitSet(new ulong[]{0x0000000030040000UL});
    public static readonly BitSet FOLLOW_28_in_constvector767 = new BitSet(new ulong[]{0x0000000030040000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector771 = new BitSet(new ulong[]{0x0000000040000000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector793 = new BitSet(new ulong[]{0x0000000040000000UL});
    public static readonly BitSet FOLLOW_30_in_constvector818 = new BitSet(new ulong[]{0x0000000030040000UL});
    public static readonly BitSet FOLLOW_28_in_constvector829 = new BitSet(new ulong[]{0x0000000030040000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector833 = new BitSet(new ulong[]{0x00000000C0000000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector855 = new BitSet(new ulong[]{0x00000000C0000000UL});
    public static readonly BitSet FOLLOW_30_in_constvector882 = new BitSet(new ulong[]{0x0000000030040000UL});
    public static readonly BitSet FOLLOW_28_in_constvector913 = new BitSet(new ulong[]{0x0000000030040000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector917 = new BitSet(new ulong[]{0x0000000080000000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector939 = new BitSet(new ulong[]{0x0000000080000000UL});
    public static readonly BitSet FOLLOW_31_in_constvector1014 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_number_in_constscalar1062 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_constvector_in_constscalar1087 = new BitSet(new ulong[]{0x0000000000000380UL});
    public static readonly BitSet FOLLOW_X_in_constscalar1106 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Y_in_constscalar1136 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Z_in_constscalar1166 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_32_in_time1219 = new BitSet(new ulong[]{0x0000000000040000UL});
    public static readonly BitSet FOLLOW_number_in_time1223 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_32_in_time1242 = new BitSet(new ulong[]{0x0000000008000000UL});
    public static readonly BitSet FOLLOW_27_in_time1244 = new BitSet(new ulong[]{0x0000000000040000UL});
    public static readonly BitSet FOLLOW_number_in_time1248 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_anchor_in_constraint1284 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_26_in_constraint1294 = new BitSet(new ulong[]{0x0000004030401820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_constraint1306 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_24_in_constraint1316 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_constraint1346 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_26_in_constraint1356 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_constraint1368 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_24_in_constraint1378 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_constraint1408 = new BitSet(new ulong[]{0x0000001E00000000UL});
    public static readonly BitSet FOLLOW_33_in_constraint1434 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_34_in_constraint1462 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_35_in_constraint1490 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_36_in_constraint1517 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_constraint1573 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_24_in_constraint1591 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr2_in_vectorexpr1640 = new BitSet(new ulong[]{0x0000000018000002UL});
    public static readonly BitSet FOLLOW_27_in_vectorexpr1692 = new BitSet(new ulong[]{0x0000004030401820UL});
    public static readonly BitSet FOLLOW_28_in_vectorexpr1720 = new BitSet(new ulong[]{0x0000004030401820UL});
    public static readonly BitSet FOLLOW_vectorexpr2_in_vectorexpr1777 = new BitSet(new ulong[]{0x0000000018000002UL});
    public static readonly BitSet FOLLOW_vectorexpr3_in_vectorexpr21892 = new BitSet(new ulong[]{0x0000002000000402UL});
    public static readonly BitSet FOLLOW_ROTATE_in_vectorexpr21912 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_vectorexpr21924 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vectorexpr21938 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_vectorexpr21957 = new BitSet(new ulong[]{0x0000002000000402UL});
    public static readonly BitSet FOLLOW_37_in_vectorexpr21963 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr4_in_vectorexpr21971 = new BitSet(new ulong[]{0x0000002000000402UL});
    public static readonly BitSet FOLLOW_28_in_vectorexpr32017 = new BitSet(new ulong[]{0x0000004030401820UL});
    public static readonly BitSet FOLLOW_vectorexpr4_in_vectorexpr32021 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr4_in_vectorexpr32037 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTEGRAL_in_vectorexpr42074 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_vectorexpr42084 = new BitSet(new ulong[]{0x0000004030401820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_vectorexpr42096 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_vectorexpr42115 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DIFFERENTIAL_in_vectorexpr42142 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_vectorexpr42160 = new BitSet(new ulong[]{0x0000004030401820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_vectorexpr42189 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_vectorexpr42208 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr5_in_vectorexpr42237 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_22_in_vectorexpr52274 = new BitSet(new ulong[]{0x0000004030401820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_vectorexpr52286 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_vectorexpr52305 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vector_in_vectorexpr52334 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_anchor_in_vectorexpr52359 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_vectorexpr52382 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_38_in_vectorexpr52408 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_29_in_vector2504 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vector2516 = new BitSet(new ulong[]{0x0000000040000000UL});
    public static readonly BitSet FOLLOW_30_in_vector2526 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vector2538 = new BitSet(new ulong[]{0x00000000C0000000UL});
    public static readonly BitSet FOLLOW_30_in_vector2550 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vector2564 = new BitSet(new ulong[]{0x0000000080000000UL});
    public static readonly BitSet FOLLOW_31_in_vector2579 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_thing_in_anchor2629 = new BitSet(new ulong[]{0x0000008000000000UL});
    public static readonly BitSet FOLLOW_39_in_anchor2641 = new BitSet(new ulong[]{0x0000000000000020UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchor2653 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_thing2697 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr2_in_scalarexpr2744 = new BitSet(new ulong[]{0x0000000018000002UL});
    public static readonly BitSet FOLLOW_27_in_scalarexpr2796 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_28_in_scalarexpr2824 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr2_in_scalarexpr2881 = new BitSet(new ulong[]{0x0000000018000002UL});
    public static readonly BitSet FOLLOW_scalarexpr3_in_scalarexpr22929 = new BitSet(new ulong[]{0x0000012000000002UL});
    public static readonly BitSet FOLLOW_37_in_scalarexpr22981 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_40_in_scalarexpr23009 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr3_in_scalarexpr23066 = new BitSet(new ulong[]{0x0000012000000002UL});
    public static readonly BitSet FOLLOW_28_in_scalarexpr33151 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr4_in_scalarexpr33155 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr4_in_scalarexpr33171 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr5_in_scalarexpr43207 = new BitSet(new ulong[]{0x0000060000000002UL});
    public static readonly BitSet FOLLOW_41_in_scalarexpr43216 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_42_in_scalarexpr43237 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SQRT_in_scalarexpr43287 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_scalarexpr43294 = new BitSet(new ulong[]{0x000000403045D820UL});
    public static readonly BitSet FOLLOW_scalarexpr5_in_scalarexpr43306 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_scalarexpr43321 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr5Ambiguous_in_scalarexpr53349 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTEGRAL_in_scalarexpr53389 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_scalarexpr53391 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr53413 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_scalarexpr53431 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DIFFERENTIAL_in_scalarexpr53457 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_scalarexpr53459 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr53477 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_scalarexpr53495 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ANGLE_in_scalarexpr53521 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_scalarexpr53523 = new BitSet(new ulong[]{0x0000004030401820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_scalarexpr53548 = new BitSet(new ulong[]{0x0000000040000000UL});
    public static readonly BitSet FOLLOW_30_in_scalarexpr53565 = new BitSet(new ulong[]{0x0000004030401820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_scalarexpr53594 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_scalarexpr53606 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_number_in_scalarexpr53634 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_scalarexpr53657 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_38_in_scalarexpr53683 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_in_scalarexpr53718 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IV_in_scalarexpr53748 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr5_in_scalarexpr5Ambiguous3849 = new BitSet(new ulong[]{0x0000000000020380UL});
    public static readonly BitSet FOLLOW_X_in_scalarexpr5Ambiguous3860 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Y_in_scalarexpr5Ambiguous3890 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Z_in_scalarexpr5Ambiguous3920 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LENGTH_in_scalarexpr5Ambiguous3950 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_22_in_scalarexpr5Ambiguous3998 = new BitSet(new ulong[]{0x000000403045F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr5Ambiguous4023 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_scalarexpr5Ambiguous4042 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NUMBER_in_number4068 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr5_in_synpred1_Movimentum3849 = new BitSet(new ulong[]{0x0000000000020380UL});
    public static readonly BitSet FOLLOW_set_in_synpred1_Movimentum3858 = new BitSet(new ulong[]{0x0000000000000002UL});

}
}