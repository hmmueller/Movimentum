// $ANTLR 3.1.1 Movimentum.g 2012-04-23 12:51:23
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
		"'/'"
    };

    public const int T__29 = 29;
    public const int T__28 = 28;
    public const int ANGLE = 13;
    public const int T__27 = 27;
    public const int T__26 = 26;
    public const int T__25 = 25;
    public const int T__24 = 24;
    public const int T__23 = 23;
    public const int T__22 = 22;
    public const int T__21 = 21;
    public const int NUMBER = 17;
    public const int T = 14;
    public const int WHITESPACE = 19;
    public const int INTEGRAL = 11;
    public const int ROTATE = 10;
    public const int EOF = -1;
    public const int COLOR = 18;
    public const int Y = 8;
    public const int LENGTH = 16;
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
    public const int IV = 15;
    public const int IDENT = 5;
    public const int COMMENT = 20;
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

            	    if ( (LA3_0 == 31) )
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
            	Match(input,21,FOLLOW_21_in_config300); if (state.failed) return result;
            	PushFollow(FOLLOW_number_in_config318);
            	fptu = number();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,22,FOLLOW_22_in_config330); if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = new Config(fptu); 
            	}
            	Match(input,23,FOLLOW_23_in_config358); if (state.failed) return result;

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
            	Match(input,24,FOLLOW_24_in_thingdefinition397); if (state.failed) return result;
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

            	Match(input,23,FOLLOW_23_in_thingdefinition455); if (state.failed) return result;
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
            	Match(input,25,FOLLOW_25_in_anchordefinition554); if (state.failed) return ;
            	 ConstVector v = null; 
            	// Movimentum.g:55:9: (c= constvector | i= IDENT '+' c= constvector | i= IDENT '-' c= constvector )
            	int alt5 = 3;
            	int LA5_0 = input.LA(1);

            	if ( (LA5_0 == 28) )
            	{
            	    alt5 = 1;
            	}
            	else if ( (LA5_0 == IDENT) )
            	{
            	    int LA5_2 = input.LA(2);

            	    if ( (LA5_2 == 26) )
            	    {
            	        alt5 = 2;
            	    }
            	    else if ( (LA5_2 == 27) )
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
            	        	Match(input,26,FOLLOW_26_in_anchordefinition626); if (state.failed) return ;
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
            	        	Match(input,27,FOLLOW_27_in_anchordefinition649); if (state.failed) return ;
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
    // Movimentum.g:61:5: constvector returns [ConstVector result] : '[' ( '-' c= constscalar | c= constscalar ) ',' ( '-' c= constscalar | c= constscalar ) ( ',' ( '-' c= constscalar | c= constscalar ) )? ']' ;
    public ConstVector constvector() // throws RecognitionException [1]
    {   
        ConstVector result = default(ConstVector);

        double c = default(double);


        try 
    	{
            // Movimentum.g:62:7: ( '[' ( '-' c= constscalar | c= constscalar ) ',' ( '-' c= constscalar | c= constscalar ) ( ',' ( '-' c= constscalar | c= constscalar ) )? ']' )
            // Movimentum.g:62:29: '[' ( '-' c= constscalar | c= constscalar ) ',' ( '-' c= constscalar | c= constscalar ) ( ',' ( '-' c= constscalar | c= constscalar ) )? ']'
            {
            	 double x = double.NaN, y = double.NaN, z = 0; 
            	Match(input,28,FOLLOW_28_in_constvector754); if (state.failed) return result;
            	// Movimentum.g:64:9: ( '-' c= constscalar | c= constscalar )
            	int alt6 = 2;
            	int LA6_0 = input.LA(1);

            	if ( (LA6_0 == 27) )
            	{
            	    alt6 = 1;
            	}
            	else if ( (LA6_0 == NUMBER || LA6_0 == 28) )
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
            	        	Match(input,27,FOLLOW_27_in_constvector765); if (state.failed) return result;
            	        	PushFollow(FOLLOW_constscalar_in_constvector769);
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
            	        	PushFollow(FOLLOW_constscalar_in_constvector789);
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

            	Match(input,29,FOLLOW_29_in_constvector812); if (state.failed) return result;
            	// Movimentum.g:68:9: ( '-' c= constscalar | c= constscalar )
            	int alt7 = 2;
            	int LA7_0 = input.LA(1);

            	if ( (LA7_0 == 27) )
            	{
            	    alt7 = 1;
            	}
            	else if ( (LA7_0 == NUMBER || LA7_0 == 28) )
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
            	        	Match(input,27,FOLLOW_27_in_constvector823); if (state.failed) return result;
            	        	PushFollow(FOLLOW_constscalar_in_constvector827);
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
            	        	PushFollow(FOLLOW_constscalar_in_constvector847);
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

            	// Movimentum.g:71:9: ( ',' ( '-' c= constscalar | c= constscalar ) )?
            	int alt9 = 2;
            	int LA9_0 = input.LA(1);

            	if ( (LA9_0 == 29) )
            	{
            	    alt9 = 1;
            	}
            	switch (alt9) 
            	{
            	    case 1 :
            	        // Movimentum.g:71:11: ',' ( '-' c= constscalar | c= constscalar )
            	        {
            	        	Match(input,29,FOLLOW_29_in_constvector872); if (state.failed) return result;
            	        	// Movimentum.g:72:11: ( '-' c= constscalar | c= constscalar )
            	        	int alt8 = 2;
            	        	int LA8_0 = input.LA(1);

            	        	if ( (LA8_0 == 27) )
            	        	{
            	        	    alt8 = 1;
            	        	}
            	        	else if ( (LA8_0 == NUMBER || LA8_0 == 28) )
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
            	        	        	Match(input,27,FOLLOW_27_in_constvector885); if (state.failed) return result;
            	        	        	PushFollow(FOLLOW_constscalar_in_constvector889);
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
            	        	        	PushFollow(FOLLOW_constscalar_in_constvector911);
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


            	        }
            	        break;

            	}

            	Match(input,30,FOLLOW_30_in_constvector941); if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = new ConstVector(x,y,z); 
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
    // $ANTLR end "constvector"


    // $ANTLR start "constscalar"
    // Movimentum.g:79:5: constscalar returns [double result] : (n= number | c= constvector ( X | Y | Z ) );
    public double constscalar() // throws RecognitionException [1]
    {   
        double result = default(double);

        double n = default(double);

        ConstVector c = default(ConstVector);


        try 
    	{
            // Movimentum.g:80:7: (n= number | c= constvector ( X | Y | Z ) )
            int alt11 = 2;
            int LA11_0 = input.LA(1);

            if ( (LA11_0 == NUMBER) )
            {
                alt11 = 1;
            }
            else if ( (LA11_0 == 28) )
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
                    // Movimentum.g:80:9: n= number
                    {
                    	PushFollow(FOLLOW_number_in_constscalar990);
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
                    // Movimentum.g:81:9: c= constvector ( X | Y | Z )
                    {
                    	PushFollow(FOLLOW_constvector_in_constscalar1015);
                    	c = constvector();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	// Movimentum.g:82:9: ( X | Y | Z )
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
                    	        // Movimentum.g:82:11: X
                    	        {
                    	        	Match(input,X,FOLLOW_X_in_constscalar1034); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = c.X; 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:83:11: Y
                    	        {
                    	        	Match(input,Y,FOLLOW_Y_in_constscalar1064); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = c.Y; 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:84:11: Z
                    	        {
                    	        	Match(input,Z,FOLLOW_Z_in_constscalar1094); if (state.failed) return result;
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
    // Movimentum.g:88:5: time : ( '@' n= number | '@' '+' n= number );
    public void time() // throws RecognitionException [1]
    {   
        double n = default(double);


        try 
    	{
            // Movimentum.g:89:7: ( '@' n= number | '@' '+' n= number )
            int alt12 = 2;
            int LA12_0 = input.LA(1);

            if ( (LA12_0 == 31) )
            {
                int LA12_1 = input.LA(2);

                if ( (LA12_1 == 26) )
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
                    // Movimentum.g:89:9: '@' n= number
                    {
                    	Match(input,31,FOLLOW_31_in_time1147); if (state.failed) return ;
                    	PushFollow(FOLLOW_number_in_time1151);
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
                    // Movimentum.g:90:9: '@' '+' n= number
                    {
                    	Match(input,31,FOLLOW_31_in_time1170); if (state.failed) return ;
                    	Match(input,26,FOLLOW_26_in_time1172); if (state.failed) return ;
                    	PushFollow(FOLLOW_number_in_time1176);
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
    // Movimentum.g:93:5: constraint returns [Constraint result] : (a= anchor '=' v= vectorexpr ';' | i= IDENT '=' s= scalarexpr ';' | i= IDENT ( '<' | '>' | '<=' | '>=' ) s= scalarexpr ';' );
    public Constraint constraint() // throws RecognitionException [1]
    {   
        Constraint result = default(Constraint);

        IToken i = null;
        Anchor a = default(Anchor);

        VectorExpr v = default(VectorExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:94:7: (a= anchor '=' v= vectorexpr ';' | i= IDENT '=' s= scalarexpr ';' | i= IDENT ( '<' | '>' | '<=' | '>=' ) s= scalarexpr ';' )
            int alt14 = 3;
            int LA14_0 = input.LA(1);

            if ( (LA14_0 == IDENT) )
            {
                switch ( input.LA(2) ) 
                {
                case 25:
                	{
                    alt14 = 2;
                    }
                    break;
                case 38:
                	{
                    alt14 = 1;
                    }
                    break;
                case 32:
                case 33:
                case 34:
                case 35:
                	{
                    alt14 = 3;
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
                    // Movimentum.g:94:9: a= anchor '=' v= vectorexpr ';'
                    {
                    	PushFollow(FOLLOW_anchor_in_constraint1212);
                    	a = anchor();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,25,FOLLOW_25_in_constraint1222); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_constraint1234);
                    	v = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_constraint1244); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new VectorEqualityConstraint(a, v); 
                    	}

                    }
                    break;
                case 2 :
                    // Movimentum.g:98:9: i= IDENT '=' s= scalarexpr ';'
                    {
                    	i=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_constraint1274); if (state.failed) return result;
                    	Match(input,25,FOLLOW_25_in_constraint1284); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_constraint1296);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_constraint1306); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ScalarEqualityConstraint(i.Text, s); 
                    	}

                    }
                    break;
                case 3 :
                    // Movimentum.g:102:9: i= IDENT ( '<' | '>' | '<=' | '>=' ) s= scalarexpr ';'
                    {
                    	i=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_constraint1336); if (state.failed) return result;
                    	 ScalarInequalityOperator op = 0; 
                    	// Movimentum.g:103:9: ( '<' | '>' | '<=' | '>=' )
                    	int alt13 = 4;
                    	switch ( input.LA(1) ) 
                    	{
                    	case 32:
                    		{
                    	    alt13 = 1;
                    	    }
                    	    break;
                    	case 33:
                    		{
                    	    alt13 = 2;
                    	    }
                    	    break;
                    	case 34:
                    		{
                    	    alt13 = 3;
                    	    }
                    	    break;
                    	case 35:
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
                    	        // Movimentum.g:103:11: '<'
                    	        {
                    	        	Match(input,32,FOLLOW_32_in_constraint1362); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.LE; 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:104:11: '>'
                    	        {
                    	        	Match(input,33,FOLLOW_33_in_constraint1390); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.GE; 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:105:11: '<='
                    	        {
                    	        	Match(input,34,FOLLOW_34_in_constraint1418); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.LT; 
                    	        	}

                    	        }
                    	        break;
                    	    case 4 :
                    	        // Movimentum.g:106:11: '>='
                    	        {
                    	        	Match(input,35,FOLLOW_35_in_constraint1445); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.GT; 
                    	        	}

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_scalarexpr_in_constraint1501);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_constraint1519); if (state.failed) return result;
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
    // Movimentum.g:112:5: vectorexpr returns [VectorExpr result] : v= vectorexpr2 ( ( '+' | '-' ) v= vectorexpr2 )* ;
    public VectorExpr vectorexpr() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);


        try 
    	{
            // Movimentum.g:113:7: (v= vectorexpr2 ( ( '+' | '-' ) v= vectorexpr2 )* )
            // Movimentum.g:113:9: v= vectorexpr2 ( ( '+' | '-' ) v= vectorexpr2 )*
            {
            	PushFollow(FOLLOW_vectorexpr2_in_vectorexpr1568);
            	v = vectorexpr2();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = v; 
            	}
            	// Movimentum.g:114:9: ( ( '+' | '-' ) v= vectorexpr2 )*
            	do 
            	{
            	    int alt16 = 2;
            	    int LA16_0 = input.LA(1);

            	    if ( ((LA16_0 >= 26 && LA16_0 <= 27)) )
            	    {
            	        alt16 = 1;
            	    }


            	    switch (alt16) 
            		{
            			case 1 :
            			    // Movimentum.g:114:29: ( '+' | '-' ) v= vectorexpr2
            			    {
            			    	 BinaryVectorOperator op = 0; 
            			    	// Movimentum.g:115:11: ( '+' | '-' )
            			    	int alt15 = 2;
            			    	int LA15_0 = input.LA(1);

            			    	if ( (LA15_0 == 26) )
            			    	{
            			    	    alt15 = 1;
            			    	}
            			    	else if ( (LA15_0 == 27) )
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
            			    	        // Movimentum.g:115:13: '+'
            			    	        {
            			    	        	Match(input,26,FOLLOW_26_in_vectorexpr1620); if (state.failed) return result;
            			    	        	 op = BinaryVectorOperator.PLUS; 

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // Movimentum.g:116:13: '-'
            			    	        {
            			    	        	Match(input,27,FOLLOW_27_in_vectorexpr1648); if (state.failed) return result;
            			    	        	 op = BinaryVectorOperator.MINUS; 

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_vectorexpr2_in_vectorexpr1705);
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
    // Movimentum.g:122:5: vectorexpr2 returns [VectorExpr result] : v= vectorexpr3 ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )* ;
    public VectorExpr vectorexpr2() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:123:7: (v= vectorexpr3 ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )* )
            // Movimentum.g:123:9: v= vectorexpr3 ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )*
            {
            	PushFollow(FOLLOW_vectorexpr3_in_vectorexpr21820);
            	v = vectorexpr3();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = v; 
            	}
            	// Movimentum.g:124:9: ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )*
            	do 
            	{
            	    int alt17 = 3;
            	    int LA17_0 = input.LA(1);

            	    if ( (LA17_0 == ROTATE) )
            	    {
            	        alt17 = 1;
            	    }
            	    else if ( (LA17_0 == 36) )
            	    {
            	        alt17 = 2;
            	    }


            	    switch (alt17) 
            		{
            			case 1 :
            			    // Movimentum.g:124:11: ROTATE '(' s= scalarexpr ')'
            			    {
            			    	Match(input,ROTATE,FOLLOW_ROTATE_in_vectorexpr21840); if (state.failed) return result;
            			    	Match(input,21,FOLLOW_21_in_vectorexpr21852); if (state.failed) return result;
            			    	PushFollow(FOLLOW_scalarexpr_in_vectorexpr21866);
            			    	s = scalarexpr();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return result;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	   result = new VectorScalarExpr(result, VectorScalarOperator.ROTATE, s); 
            			    	}
            			    	Match(input,22,FOLLOW_22_in_vectorexpr21885); if (state.failed) return result;

            			    }
            			    break;
            			case 2 :
            			    // Movimentum.g:128:5: '*' s= scalarexpr4
            			    {
            			    	Match(input,36,FOLLOW_36_in_vectorexpr21891); if (state.failed) return result;
            			    	PushFollow(FOLLOW_scalarexpr4_in_vectorexpr21899);
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
    // Movimentum.g:133:5: vectorexpr3 returns [VectorExpr result] : ( '-' v= vectorexpr4 | v= vectorexpr4 );
    public VectorExpr vectorexpr3() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);


        try 
    	{
            // Movimentum.g:134:7: ( '-' v= vectorexpr4 | v= vectorexpr4 )
            int alt18 = 2;
            int LA18_0 = input.LA(1);

            if ( (LA18_0 == 27) )
            {
                alt18 = 1;
            }
            else if ( (LA18_0 == IDENT || (LA18_0 >= INTEGRAL && LA18_0 <= DIFFERENTIAL) || LA18_0 == 21 || LA18_0 == 28 || LA18_0 == 37) )
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
                    // Movimentum.g:134:9: '-' v= vectorexpr4
                    {
                    	Match(input,27,FOLLOW_27_in_vectorexpr31945); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr4_in_vectorexpr31949);
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
                    // Movimentum.g:135:9: v= vectorexpr4
                    {
                    	PushFollow(FOLLOW_vectorexpr4_in_vectorexpr31965);
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
    // Movimentum.g:138:5: vectorexpr4 returns [VectorExpr result] : ( INTEGRAL '(' v= vectorexpr ')' | DIFFERENTIAL '(' v= vectorexpr ')' | v= vectorexpr5 );
    public VectorExpr vectorexpr4() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);


        try 
    	{
            // Movimentum.g:139:7: ( INTEGRAL '(' v= vectorexpr ')' | DIFFERENTIAL '(' v= vectorexpr ')' | v= vectorexpr5 )
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
            case 21:
            case 28:
            case 37:
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
                    // Movimentum.g:139:9: INTEGRAL '(' v= vectorexpr ')'
                    {
                    	Match(input,INTEGRAL,FOLLOW_INTEGRAL_in_vectorexpr42002); if (state.failed) return result;
                    	Match(input,21,FOLLOW_21_in_vectorexpr42012); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_vectorexpr42024);
                    	v = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryVectorExpr(UnaryVectorOperator.INTEGRAL, v); 
                    	}
                    	Match(input,22,FOLLOW_22_in_vectorexpr42043); if (state.failed) return result;

                    }
                    break;
                case 2 :
                    // Movimentum.g:143:9: DIFFERENTIAL '(' v= vectorexpr ')'
                    {
                    	Match(input,DIFFERENTIAL,FOLLOW_DIFFERENTIAL_in_vectorexpr42070); if (state.failed) return result;
                    	Match(input,21,FOLLOW_21_in_vectorexpr42088); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_vectorexpr42117);
                    	v = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryVectorExpr(UnaryVectorOperator.DIFFERENTIAL, v); 
                    	}
                    	Match(input,22,FOLLOW_22_in_vectorexpr42136); if (state.failed) return result;

                    }
                    break;
                case 3 :
                    // Movimentum.g:147:9: v= vectorexpr5
                    {
                    	PushFollow(FOLLOW_vectorexpr5_in_vectorexpr42165);
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
    // Movimentum.g:150:5: vectorexpr5 returns [VectorExpr result] : ( '(' e= vectorexpr ')' | v= vector | a= anchor | IDENT | '_' );
    public VectorExpr vectorexpr5() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        IToken IDENT3 = null;
        VectorExpr e = default(VectorExpr);

        Vector v = default(Vector);

        Anchor a = default(Anchor);


        try 
    	{
            // Movimentum.g:151:7: ( '(' e= vectorexpr ')' | v= vector | a= anchor | IDENT | '_' )
            int alt20 = 5;
            switch ( input.LA(1) ) 
            {
            case 21:
            	{
                alt20 = 1;
                }
                break;
            case 28:
            	{
                alt20 = 2;
                }
                break;
            case IDENT:
            	{
                int LA20_3 = input.LA(2);

                if ( (LA20_3 == 38) )
                {
                    alt20 = 3;
                }
                else if ( ((LA20_3 >= X && LA20_3 <= ROTATE) || LA20_3 == LENGTH || (LA20_3 >= 22 && LA20_3 <= 23) || (LA20_3 >= 26 && LA20_3 <= 27) || LA20_3 == 29 || LA20_3 == 36) )
                {
                    alt20 = 4;
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
            case 37:
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
                    // Movimentum.g:151:9: '(' e= vectorexpr ')'
                    {
                    	Match(input,21,FOLLOW_21_in_vectorexpr52202); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_vectorexpr52214);
                    	e = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = e; 
                    	}
                    	Match(input,22,FOLLOW_22_in_vectorexpr52233); if (state.failed) return result;

                    }
                    break;
                case 2 :
                    // Movimentum.g:154:9: v= vector
                    {
                    	PushFollow(FOLLOW_vector_in_vectorexpr52262);
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
                    // Movimentum.g:155:9: a= anchor
                    {
                    	PushFollow(FOLLOW_anchor_in_vectorexpr52287);
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
                    // Movimentum.g:156:9: IDENT
                    {
                    	IDENT3=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_vectorexpr52310); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new VectorVariable(IDENT3.Text); 
                    	}

                    }
                    break;
                case 5 :
                    // Movimentum.g:157:9: '_'
                    {
                    	Match(input,37,FOLLOW_37_in_vectorexpr52336); if (state.failed) return result;
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
    // Movimentum.g:160:5: vector returns [Vector result] : '[' s1= scalarexpr ',' s2= scalarexpr ( ',' s3= scalarexpr )? ']' ;
    public Vector vector() // throws RecognitionException [1]
    {   
        Vector result = default(Vector);

        ScalarExpr s1 = default(ScalarExpr);

        ScalarExpr s2 = default(ScalarExpr);

        ScalarExpr s3 = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:161:7: ( '[' s1= scalarexpr ',' s2= scalarexpr ( ',' s3= scalarexpr )? ']' )
            // Movimentum.g:161:9: '[' s1= scalarexpr ',' s2= scalarexpr ( ',' s3= scalarexpr )? ']'
            {
            	Match(input,28,FOLLOW_28_in_vector2432); if (state.failed) return result;
            	PushFollow(FOLLOW_scalarexpr_in_vector2444);
            	s1 = scalarexpr();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,29,FOLLOW_29_in_vector2454); if (state.failed) return result;
            	PushFollow(FOLLOW_scalarexpr_in_vector2466);
            	s2 = scalarexpr();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	// Movimentum.g:165:9: ( ',' s3= scalarexpr )?
            	int alt21 = 2;
            	int LA21_0 = input.LA(1);

            	if ( (LA21_0 == 29) )
            	{
            	    alt21 = 1;
            	}
            	switch (alt21) 
            	{
            	    case 1 :
            	        // Movimentum.g:165:11: ',' s3= scalarexpr
            	        {
            	        	Match(input,29,FOLLOW_29_in_vector2478); if (state.failed) return result;
            	        	PushFollow(FOLLOW_scalarexpr_in_vector2492);
            	        	s3 = scalarexpr();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return result;

            	        }
            	        break;

            	}

            	Match(input,30,FOLLOW_30_in_vector2507); if (state.failed) return result;
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
    // Movimentum.g:171:5: anchor returns [Anchor result] : th= thing '.' IDENT ;
    public Anchor anchor() // throws RecognitionException [1]
    {   
        Anchor result = default(Anchor);

        IToken IDENT4 = null;
        string th = default(string);


        try 
    	{
            // Movimentum.g:172:7: (th= thing '.' IDENT )
            // Movimentum.g:172:10: th= thing '.' IDENT
            {
            	PushFollow(FOLLOW_thing_in_anchor2557);
            	th = thing();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,38,FOLLOW_38_in_anchor2569); if (state.failed) return result;
            	IDENT4=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_anchor2581); if (state.failed) return result;
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
    // Movimentum.g:177:5: thing returns [string result] : IDENT ;
    public string thing() // throws RecognitionException [1]
    {   
        string result = default(string);

        IToken IDENT5 = null;

        try 
    	{
            // Movimentum.g:178:7: ( IDENT )
            // Movimentum.g:178:9: IDENT
            {
            	IDENT5=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_thing2625); if (state.failed) return result;
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
    // Movimentum.g:181:5: scalarexpr returns [ScalarExpr result] : s= scalarexpr2 ( ( '+' | '-' ) s= scalarexpr2 )* ;
    public ScalarExpr scalarexpr() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:182:7: (s= scalarexpr2 ( ( '+' | '-' ) s= scalarexpr2 )* )
            // Movimentum.g:182:9: s= scalarexpr2 ( ( '+' | '-' ) s= scalarexpr2 )*
            {
            	PushFollow(FOLLOW_scalarexpr2_in_scalarexpr2672);
            	s = scalarexpr2();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = s; 
            	}
            	// Movimentum.g:183:9: ( ( '+' | '-' ) s= scalarexpr2 )*
            	do 
            	{
            	    int alt23 = 2;
            	    int LA23_0 = input.LA(1);

            	    if ( ((LA23_0 >= 26 && LA23_0 <= 27)) )
            	    {
            	        alt23 = 1;
            	    }


            	    switch (alt23) 
            		{
            			case 1 :
            			    // Movimentum.g:183:29: ( '+' | '-' ) s= scalarexpr2
            			    {
            			    	 BinaryScalarOperator op = 0; 
            			    	// Movimentum.g:184:11: ( '+' | '-' )
            			    	int alt22 = 2;
            			    	int LA22_0 = input.LA(1);

            			    	if ( (LA22_0 == 26) )
            			    	{
            			    	    alt22 = 1;
            			    	}
            			    	else if ( (LA22_0 == 27) )
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
            			    	        // Movimentum.g:184:13: '+'
            			    	        {
            			    	        	Match(input,26,FOLLOW_26_in_scalarexpr2724); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.PLUS; 

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // Movimentum.g:185:13: '-'
            			    	        {
            			    	        	Match(input,27,FOLLOW_27_in_scalarexpr2752); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.MINUS; 

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_scalarexpr2_in_scalarexpr2809);
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
    // Movimentum.g:191:5: scalarexpr2 returns [ScalarExpr result] : s= scalarexpr3 ( ( '*' | '/' ) s= scalarexpr3 )* ;
    public ScalarExpr scalarexpr2() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:192:7: (s= scalarexpr3 ( ( '*' | '/' ) s= scalarexpr3 )* )
            // Movimentum.g:192:9: s= scalarexpr3 ( ( '*' | '/' ) s= scalarexpr3 )*
            {
            	PushFollow(FOLLOW_scalarexpr3_in_scalarexpr22857);
            	s = scalarexpr3();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = s; 
            	}
            	// Movimentum.g:193:9: ( ( '*' | '/' ) s= scalarexpr3 )*
            	do 
            	{
            	    int alt25 = 2;
            	    int LA25_0 = input.LA(1);

            	    if ( (LA25_0 == 36 || LA25_0 == 39) )
            	    {
            	        alt25 = 1;
            	    }


            	    switch (alt25) 
            		{
            			case 1 :
            			    // Movimentum.g:193:29: ( '*' | '/' ) s= scalarexpr3
            			    {
            			    	 BinaryScalarOperator op = 0; 
            			    	// Movimentum.g:194:11: ( '*' | '/' )
            			    	int alt24 = 2;
            			    	int LA24_0 = input.LA(1);

            			    	if ( (LA24_0 == 36) )
            			    	{
            			    	    alt24 = 1;
            			    	}
            			    	else if ( (LA24_0 == 39) )
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
            			    	        // Movimentum.g:194:13: '*'
            			    	        {
            			    	        	Match(input,36,FOLLOW_36_in_scalarexpr22909); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.TIMES; 

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // Movimentum.g:195:13: '/'
            			    	        {
            			    	        	Match(input,39,FOLLOW_39_in_scalarexpr22937); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.DIVIDE; 

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_scalarexpr3_in_scalarexpr22994);
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
    // Movimentum.g:201:5: scalarexpr3 returns [ScalarExpr result] : ( '-' s= scalarexpr4 | s= scalarexpr4 );
    public ScalarExpr scalarexpr3() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:202:7: ( '-' s= scalarexpr4 | s= scalarexpr4 )
            int alt26 = 2;
            int LA26_0 = input.LA(1);

            if ( (LA26_0 == 27) )
            {
                alt26 = 1;
            }
            else if ( (LA26_0 == IDENT || (LA26_0 >= INTEGRAL && LA26_0 <= IV) || LA26_0 == NUMBER || LA26_0 == 21 || LA26_0 == 28 || LA26_0 == 37) )
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
                    // Movimentum.g:202:9: '-' s= scalarexpr4
                    {
                    	Match(input,27,FOLLOW_27_in_scalarexpr33079); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr4_in_scalarexpr33083);
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
                    // Movimentum.g:203:9: s= scalarexpr4
                    {
                    	PushFollow(FOLLOW_scalarexpr4_in_scalarexpr33099);
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
    // Movimentum.g:206:5: scalarexpr4 returns [ScalarExpr result] : (s= scalarexpr4Ambiguous | INTEGRAL '(' s= scalarexpr ')' | DIFFERENTIAL '(' s= scalarexpr ')' | ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')' | n= number | IDENT | '_' | T | IV );
    public ScalarExpr scalarexpr4() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        IToken IDENT6 = null;
        ScalarExpr s = default(ScalarExpr);

        VectorExpr v1 = default(VectorExpr);

        VectorExpr v2 = default(VectorExpr);

        double n = default(double);


        try 
    	{
            // Movimentum.g:207:7: (s= scalarexpr4Ambiguous | INTEGRAL '(' s= scalarexpr ')' | DIFFERENTIAL '(' s= scalarexpr ')' | ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')' | n= number | IDENT | '_' | T | IV )
            int alt27 = 9;
            alt27 = dfa27.Predict(input);
            switch (alt27) 
            {
                case 1 :
                    // Movimentum.g:207:9: s= scalarexpr4Ambiguous
                    {
                    	PushFollow(FOLLOW_scalarexpr4Ambiguous_in_scalarexpr43138);
                    	s = scalarexpr4Ambiguous();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = s; 
                    	}

                    }
                    break;
                case 2 :
                    // Movimentum.g:209:9: INTEGRAL '(' s= scalarexpr ')'
                    {
                    	Match(input,INTEGRAL,FOLLOW_INTEGRAL_in_scalarexpr43178); if (state.failed) return result;
                    	Match(input,21,FOLLOW_21_in_scalarexpr43180); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr43202);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,22,FOLLOW_22_in_scalarexpr43220); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryScalarExpr(UnaryScalarOperator.INTEGRAL, s); 
                    	}

                    }
                    break;
                case 3 :
                    // Movimentum.g:212:9: DIFFERENTIAL '(' s= scalarexpr ')'
                    {
                    	Match(input,DIFFERENTIAL,FOLLOW_DIFFERENTIAL_in_scalarexpr43246); if (state.failed) return result;
                    	Match(input,21,FOLLOW_21_in_scalarexpr43248); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr43266);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,22,FOLLOW_22_in_scalarexpr43284); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryScalarExpr(UnaryScalarOperator.DIFFERENTIAL, s); 
                    	}

                    }
                    break;
                case 4 :
                    // Movimentum.g:215:9: ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')'
                    {
                    	Match(input,ANGLE,FOLLOW_ANGLE_in_scalarexpr43310); if (state.failed) return result;
                    	Match(input,21,FOLLOW_21_in_scalarexpr43312); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_scalarexpr43337);
                    	v1 = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,29,FOLLOW_29_in_scalarexpr43354); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_scalarexpr43383);
                    	v2 = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,22,FOLLOW_22_in_scalarexpr43395); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new BinaryScalarVectorExpr(v1, BinaryScalarVectorOperator.ANGLE, v2); 
                    	}

                    }
                    break;
                case 5 :
                    // Movimentum.g:220:9: n= number
                    {
                    	PushFollow(FOLLOW_number_in_scalarexpr43423);
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
                    // Movimentum.g:221:9: IDENT
                    {
                    	IDENT6=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_scalarexpr43446); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ScalarVariable(IDENT6.Text); 
                    	}

                    }
                    break;
                case 7 :
                    // Movimentum.g:222:9: '_'
                    {
                    	Match(input,37,FOLLOW_37_in_scalarexpr43472); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ScalarVariable("_" + _anonymousVarCt++); 
                    	}

                    }
                    break;
                case 8 :
                    // Movimentum.g:224:9: T
                    {
                    	Match(input,T,FOLLOW_T_in_scalarexpr43507); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new T(); 
                    	}

                    }
                    break;
                case 9 :
                    // Movimentum.g:225:9: IV
                    {
                    	Match(input,IV,FOLLOW_IV_in_scalarexpr43537); if (state.failed) return result;
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
    // $ANTLR end "scalarexpr4"


    // $ANTLR start "scalarexpr4Ambiguous"
    // Movimentum.g:228:4: scalarexpr4Ambiguous returns [ScalarExpr result] options {backtrack=true; } : (v= vectorexpr5 ( X | Y | Z | LENGTH ) | '(' s= scalarexpr ')' );
    public ScalarExpr scalarexpr4Ambiguous() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        VectorExpr v = default(VectorExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:230:6: (v= vectorexpr5 ( X | Y | Z | LENGTH ) | '(' s= scalarexpr ')' )
            int alt29 = 2;
            int LA29_0 = input.LA(1);

            if ( (LA29_0 == 21) )
            {
                int LA29_1 = input.LA(2);

                if ( (synpred1_Movimentum()) )
                {
                    alt29 = 1;
                }
                else if ( (true) )
                {
                    alt29 = 2;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    NoViableAltException nvae_d29s1 =
                        new NoViableAltException("", 29, 1, input);

                    throw nvae_d29s1;
                }
            }
            else if ( (LA29_0 == IDENT || LA29_0 == 28 || LA29_0 == 37) )
            {
                alt29 = 1;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d29s0 =
                    new NoViableAltException("", 29, 0, input);

                throw nvae_d29s0;
            }
            switch (alt29) 
            {
                case 1 :
                    // Movimentum.g:230:9: v= vectorexpr5 ( X | Y | Z | LENGTH )
                    {
                    	PushFollow(FOLLOW_vectorexpr5_in_scalarexpr4Ambiguous3638);
                    	v = vectorexpr5();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	// Movimentum.g:231:8: ( X | Y | Z | LENGTH )
                    	int alt28 = 4;
                    	switch ( input.LA(1) ) 
                    	{
                    	case X:
                    		{
                    	    alt28 = 1;
                    	    }
                    	    break;
                    	case Y:
                    		{
                    	    alt28 = 2;
                    	    }
                    	    break;
                    	case Z:
                    		{
                    	    alt28 = 3;
                    	    }
                    	    break;
                    	case LENGTH:
                    		{
                    	    alt28 = 4;
                    	    }
                    	    break;
                    		default:
                    		    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    		    NoViableAltException nvae_d28s0 =
                    		        new NoViableAltException("", 28, 0, input);

                    		    throw nvae_d28s0;
                    	}

                    	switch (alt28) 
                    	{
                    	    case 1 :
                    	        // Movimentum.g:231:10: X
                    	        {
                    	        	Match(input,X,FOLLOW_X_in_scalarexpr4Ambiguous3649); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.X); 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:232:10: Y
                    	        {
                    	        	Match(input,Y,FOLLOW_Y_in_scalarexpr4Ambiguous3679); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.Y); 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:233:10: Z
                    	        {
                    	        	Match(input,Z,FOLLOW_Z_in_scalarexpr4Ambiguous3709); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarVectorExpr(v, UnaryScalarVectorOperator.Z); 
                    	        	}

                    	        }
                    	        break;
                    	    case 4 :
                    	        // Movimentum.g:234:10: LENGTH
                    	        {
                    	        	Match(input,LENGTH,FOLLOW_LENGTH_in_scalarexpr4Ambiguous3739); if (state.failed) return result;
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
                    // Movimentum.g:236:8: '(' s= scalarexpr ')'
                    {
                    	Match(input,21,FOLLOW_21_in_scalarexpr4Ambiguous3787); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr4Ambiguous3812);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = s; 
                    	}
                    	Match(input,22,FOLLOW_22_in_scalarexpr4Ambiguous3831); if (state.failed) return result;

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
    // $ANTLR end "scalarexpr4Ambiguous"


    // $ANTLR start "number"
    // Movimentum.g:241:4: number returns [double value] : NUMBER ;
    public double number() // throws RecognitionException [1]
    {   
        double value = default(double);

        IToken NUMBER7 = null;

        try 
    	{
            // Movimentum.g:242:6: ( NUMBER )
            // Movimentum.g:242:8: NUMBER
            {
            	NUMBER7=(IToken)Match(input,NUMBER,FOLLOW_NUMBER_in_number3857); if (state.failed) return value;
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


        // Movimentum.g:230:9: (v= vectorexpr5 ( X | Y | Z | LENGTH ) )
        // Movimentum.g:230:9: v= vectorexpr5 ( X | Y | Z | LENGTH )
        {
        	PushFollow(FOLLOW_vectorexpr5_in_synpred1_Movimentum3638);
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


   	protected DFA27 dfa27;
	private void InitializeCyclicDFAs()
	{
    	this.dfa27 = new DFA27(this);
	}

    const string DFA27_eotS =
        "\x0c\uffff";
    const string DFA27_eofS =
        "\x0c\uffff";
    const string DFA27_minS =
        "\x01\x05\x01\uffff\x02\x07\x08\uffff";
    const string DFA27_maxS =
        "\x01\x25\x01\uffff\x02\x27\x08\uffff";
    const string DFA27_acceptS =
        "\x01\uffff\x01\x01\x02\uffff\x01\x02\x01\x03\x01\x04\x01\x05\x01"+
        "\x08\x01\x09\x01\x06\x01\x07";
    const string DFA27_specialS =
        "\x0c\uffff}>";
    static readonly string[] DFA27_transitionS = {
            "\x01\x02\x05\uffff\x01\x04\x01\x05\x01\x06\x01\x08\x01\x09"+
            "\x01\uffff\x01\x07\x03\uffff\x01\x01\x06\uffff\x01\x01\x08\uffff"+
            "\x01\x03",
            "",
            "\x03\x01\x01\x0a\x05\uffff\x01\x01\x05\uffff\x02\x0a\x02\uffff"+
            "\x02\x0a\x01\uffff\x02\x0a\x05\uffff\x01\x0a\x01\uffff\x01\x01"+
            "\x01\x0a",
            "\x03\x01\x01\x0b\x05\uffff\x01\x01\x05\uffff\x02\x0b\x02\uffff"+
            "\x02\x0b\x01\uffff\x02\x0b\x05\uffff\x01\x0b\x02\uffff\x01\x0b",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA27_eot = DFA.UnpackEncodedString(DFA27_eotS);
    static readonly short[] DFA27_eof = DFA.UnpackEncodedString(DFA27_eofS);
    static readonly char[] DFA27_min = DFA.UnpackEncodedStringToUnsignedChars(DFA27_minS);
    static readonly char[] DFA27_max = DFA.UnpackEncodedStringToUnsignedChars(DFA27_maxS);
    static readonly short[] DFA27_accept = DFA.UnpackEncodedString(DFA27_acceptS);
    static readonly short[] DFA27_special = DFA.UnpackEncodedString(DFA27_specialS);
    static readonly short[][] DFA27_transition = DFA.UnpackEncodedStringArray(DFA27_transitionS);

    protected class DFA27 : DFA
    {
        public DFA27(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 27;
            this.eot = DFA27_eot;
            this.eof = DFA27_eof;
            this.min = DFA27_min;
            this.max = DFA27_max;
            this.accept = DFA27_accept;
            this.special = DFA27_special;
            this.transition = DFA27_transition;

        }

        override public string Description
        {
            get { return "206:5: scalarexpr4 returns [ScalarExpr result] : (s= scalarexpr4Ambiguous | INTEGRAL '(' s= scalarexpr ')' | DIFFERENTIAL '(' s= scalarexpr ')' | ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')' | n= number | IDENT | '_' | T | IV );"; }
        }

    }

 

    public static readonly BitSet FOLLOW_config_in_script124 = new BitSet(new ulong[]{0x0000000080000020UL});
    public static readonly BitSet FOLLOW_thingdefinition_in_script138 = new BitSet(new ulong[]{0x0000000080000020UL});
    public static readonly BitSet FOLLOW_time_in_script163 = new BitSet(new ulong[]{0x0000000080000020UL});
    public static readonly BitSet FOLLOW_constraint_in_script194 = new BitSet(new ulong[]{0x0000000080000020UL});
    public static readonly BitSet FOLLOW_EOF_in_script269 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CONFIG_in_config298 = new BitSet(new ulong[]{0x0000000000200000UL});
    public static readonly BitSet FOLLOW_21_in_config300 = new BitSet(new ulong[]{0x0000000000020000UL});
    public static readonly BitSet FOLLOW_number_in_config318 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_config330 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_config358 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_thingdefinition387 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_24_in_thingdefinition397 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_source_in_thingdefinition409 = new BitSet(new ulong[]{0x0000000000000020UL});
    public static readonly BitSet FOLLOW_anchordefinition_in_thingdefinition433 = new BitSet(new ulong[]{0x0000000000800020UL});
    public static readonly BitSet FOLLOW_23_in_thingdefinition455 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_FILENAME_in_source502 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchordefinition544 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_anchordefinition554 = new BitSet(new ulong[]{0x0000000010000020UL});
    public static readonly BitSet FOLLOW_constvector_in_anchordefinition595 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchordefinition624 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_26_in_anchordefinition626 = new BitSet(new ulong[]{0x0000000010000000UL});
    public static readonly BitSet FOLLOW_constvector_in_anchordefinition630 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchordefinition647 = new BitSet(new ulong[]{0x0000000008000000UL});
    public static readonly BitSet FOLLOW_27_in_anchordefinition649 = new BitSet(new ulong[]{0x0000000010000000UL});
    public static readonly BitSet FOLLOW_constvector_in_anchordefinition653 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_28_in_constvector754 = new BitSet(new ulong[]{0x0000000018020000UL});
    public static readonly BitSet FOLLOW_27_in_constvector765 = new BitSet(new ulong[]{0x0000000018020000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector769 = new BitSet(new ulong[]{0x0000000020000000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector789 = new BitSet(new ulong[]{0x0000000020000000UL});
    public static readonly BitSet FOLLOW_29_in_constvector812 = new BitSet(new ulong[]{0x0000000018020000UL});
    public static readonly BitSet FOLLOW_27_in_constvector823 = new BitSet(new ulong[]{0x0000000018020000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector827 = new BitSet(new ulong[]{0x0000000060000000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector847 = new BitSet(new ulong[]{0x0000000060000000UL});
    public static readonly BitSet FOLLOW_29_in_constvector872 = new BitSet(new ulong[]{0x0000000018020000UL});
    public static readonly BitSet FOLLOW_27_in_constvector885 = new BitSet(new ulong[]{0x0000000018020000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector889 = new BitSet(new ulong[]{0x0000000040000000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector911 = new BitSet(new ulong[]{0x0000000040000000UL});
    public static readonly BitSet FOLLOW_30_in_constvector941 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_number_in_constscalar990 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_constvector_in_constscalar1015 = new BitSet(new ulong[]{0x0000000000000380UL});
    public static readonly BitSet FOLLOW_X_in_constscalar1034 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Y_in_constscalar1064 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Z_in_constscalar1094 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_31_in_time1147 = new BitSet(new ulong[]{0x0000000000020000UL});
    public static readonly BitSet FOLLOW_number_in_time1151 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_31_in_time1170 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_26_in_time1172 = new BitSet(new ulong[]{0x0000000000020000UL});
    public static readonly BitSet FOLLOW_number_in_time1176 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_anchor_in_constraint1212 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_constraint1222 = new BitSet(new ulong[]{0x0000002018201820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_constraint1234 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_constraint1244 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_constraint1274 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_constraint1284 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_constraint1296 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_constraint1306 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_constraint1336 = new BitSet(new ulong[]{0x0000000F00000000UL});
    public static readonly BitSet FOLLOW_32_in_constraint1362 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_33_in_constraint1390 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_34_in_constraint1418 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_35_in_constraint1445 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_constraint1501 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_constraint1519 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr2_in_vectorexpr1568 = new BitSet(new ulong[]{0x000000000C000002UL});
    public static readonly BitSet FOLLOW_26_in_vectorexpr1620 = new BitSet(new ulong[]{0x0000002018201820UL});
    public static readonly BitSet FOLLOW_27_in_vectorexpr1648 = new BitSet(new ulong[]{0x0000002018201820UL});
    public static readonly BitSet FOLLOW_vectorexpr2_in_vectorexpr1705 = new BitSet(new ulong[]{0x000000000C000002UL});
    public static readonly BitSet FOLLOW_vectorexpr3_in_vectorexpr21820 = new BitSet(new ulong[]{0x0000001000000402UL});
    public static readonly BitSet FOLLOW_ROTATE_in_vectorexpr21840 = new BitSet(new ulong[]{0x0000000000200000UL});
    public static readonly BitSet FOLLOW_21_in_vectorexpr21852 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vectorexpr21866 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_vectorexpr21885 = new BitSet(new ulong[]{0x0000001000000402UL});
    public static readonly BitSet FOLLOW_36_in_vectorexpr21891 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr4_in_vectorexpr21899 = new BitSet(new ulong[]{0x0000001000000402UL});
    public static readonly BitSet FOLLOW_27_in_vectorexpr31945 = new BitSet(new ulong[]{0x0000002018201820UL});
    public static readonly BitSet FOLLOW_vectorexpr4_in_vectorexpr31949 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr4_in_vectorexpr31965 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTEGRAL_in_vectorexpr42002 = new BitSet(new ulong[]{0x0000000000200000UL});
    public static readonly BitSet FOLLOW_21_in_vectorexpr42012 = new BitSet(new ulong[]{0x0000002018201820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_vectorexpr42024 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_vectorexpr42043 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DIFFERENTIAL_in_vectorexpr42070 = new BitSet(new ulong[]{0x0000000000200000UL});
    public static readonly BitSet FOLLOW_21_in_vectorexpr42088 = new BitSet(new ulong[]{0x0000002018201820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_vectorexpr42117 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_vectorexpr42136 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr5_in_vectorexpr42165 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_21_in_vectorexpr52202 = new BitSet(new ulong[]{0x0000002018201820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_vectorexpr52214 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_vectorexpr52233 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vector_in_vectorexpr52262 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_anchor_in_vectorexpr52287 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_vectorexpr52310 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_37_in_vectorexpr52336 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_28_in_vector2432 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vector2444 = new BitSet(new ulong[]{0x0000000020000000UL});
    public static readonly BitSet FOLLOW_29_in_vector2454 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vector2466 = new BitSet(new ulong[]{0x0000000060000000UL});
    public static readonly BitSet FOLLOW_29_in_vector2478 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vector2492 = new BitSet(new ulong[]{0x0000000040000000UL});
    public static readonly BitSet FOLLOW_30_in_vector2507 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_thing_in_anchor2557 = new BitSet(new ulong[]{0x0000004000000000UL});
    public static readonly BitSet FOLLOW_38_in_anchor2569 = new BitSet(new ulong[]{0x0000000000000020UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchor2581 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_thing2625 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr2_in_scalarexpr2672 = new BitSet(new ulong[]{0x000000000C000002UL});
    public static readonly BitSet FOLLOW_26_in_scalarexpr2724 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_27_in_scalarexpr2752 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr2_in_scalarexpr2809 = new BitSet(new ulong[]{0x000000000C000002UL});
    public static readonly BitSet FOLLOW_scalarexpr3_in_scalarexpr22857 = new BitSet(new ulong[]{0x0000009000000002UL});
    public static readonly BitSet FOLLOW_36_in_scalarexpr22909 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_39_in_scalarexpr22937 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr3_in_scalarexpr22994 = new BitSet(new ulong[]{0x0000009000000002UL});
    public static readonly BitSet FOLLOW_27_in_scalarexpr33079 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr4_in_scalarexpr33083 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr4_in_scalarexpr33099 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr4Ambiguous_in_scalarexpr43138 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTEGRAL_in_scalarexpr43178 = new BitSet(new ulong[]{0x0000000000200000UL});
    public static readonly BitSet FOLLOW_21_in_scalarexpr43180 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr43202 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_scalarexpr43220 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DIFFERENTIAL_in_scalarexpr43246 = new BitSet(new ulong[]{0x0000000000200000UL});
    public static readonly BitSet FOLLOW_21_in_scalarexpr43248 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr43266 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_scalarexpr43284 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ANGLE_in_scalarexpr43310 = new BitSet(new ulong[]{0x0000000000200000UL});
    public static readonly BitSet FOLLOW_21_in_scalarexpr43312 = new BitSet(new ulong[]{0x0000002018201820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_scalarexpr43337 = new BitSet(new ulong[]{0x0000000020000000UL});
    public static readonly BitSet FOLLOW_29_in_scalarexpr43354 = new BitSet(new ulong[]{0x0000002018201820UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_scalarexpr43383 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_scalarexpr43395 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_number_in_scalarexpr43423 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_scalarexpr43446 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_37_in_scalarexpr43472 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_in_scalarexpr43507 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IV_in_scalarexpr43537 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr5_in_scalarexpr4Ambiguous3638 = new BitSet(new ulong[]{0x0000000000010380UL});
    public static readonly BitSet FOLLOW_X_in_scalarexpr4Ambiguous3649 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Y_in_scalarexpr4Ambiguous3679 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Z_in_scalarexpr4Ambiguous3709 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LENGTH_in_scalarexpr4Ambiguous3739 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_21_in_scalarexpr4Ambiguous3787 = new BitSet(new ulong[]{0x000000201822F820UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr4Ambiguous3812 = new BitSet(new ulong[]{0x0000000000400000UL});
    public static readonly BitSet FOLLOW_22_in_scalarexpr4Ambiguous3831 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NUMBER_in_number3857 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr5_in_synpred1_Movimentum3638 = new BitSet(new ulong[]{0x0000000000010380UL});
    public static readonly BitSet FOLLOW_set_in_synpred1_Movimentum3647 = new BitSet(new ulong[]{0x0000000000000002UL});

}
}