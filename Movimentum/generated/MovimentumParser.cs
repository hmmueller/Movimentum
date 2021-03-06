// $ANTLR 3.1.1 Movimentum.g 2012-05-01 00:11:55
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
		"BAR", 
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
		"','", 
		"')'", 
		"';'", 
		"':'", 
		"'='", 
		"'+'", 
		"'-'", 
		"'['", 
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
		"'^3'", 
		"'{'", 
		"'}'"
    };

    public const int T__29 = 29;
    public const int T__28 = 28;
    public const int T__27 = 27;
    public const int T__26 = 26;
    public const int T__25 = 25;
    public const int T__24 = 24;
    public const int T__23 = 23;
    public const int INTEGRAL = 12;
    public const int EOF = -1;
    public const int COLOR = 20;
    public const int LENGTH = 18;
    public const int FILENAME = 6;
    public const int IV = 17;
    public const int IDENT = 5;
    public const int COMMENT = 22;
    public const int T__42 = 42;
    public const int T__43 = 43;
    public const int T__40 = 40;
    public const int T__41 = 41;
    public const int T__44 = 44;
    public const int ANGLE = 15;
    public const int T__45 = 45;
    public const int NUMBER = 19;
    public const int T = 16;
    public const int WHITESPACE = 21;
    public const int ROTATE = 11;
    public const int SQRT = 14;
    public const int Y = 9;
    public const int X = 8;
    public const int Z = 10;
    public const int T__30 = 30;
    public const int T__31 = 31;
    public const int CONFIG = 4;
    public const int T__32 = 32;
    public const int T__33 = 33;
    public const int T__34 = 34;
    public const int T__35 = 35;
    public const int T__36 = 36;
    public const int T__37 = 37;
    public const int T__38 = 38;
    public const int T__39 = 39;
    public const int BAR = 7;
    public const int DIFFERENTIAL = 13;

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

            	    if ( (LA3_0 == 33) )
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
    // Movimentum.g:32:5: config returns [Config result] : CONFIG '(' fptu= number ',' width= number ',' height= number ')' ';' ;
    public Config config() // throws RecognitionException [1]
    {   
        Config result = default(Config);

        double fptu = default(double);

        double width = default(double);

        double height = default(double);


        try 
    	{
            // Movimentum.g:33:7: ( CONFIG '(' fptu= number ',' width= number ',' height= number ')' ';' )
            // Movimentum.g:33:9: CONFIG '(' fptu= number ',' width= number ',' height= number ')' ';'
            {
            	Match(input,CONFIG,FOLLOW_CONFIG_in_config298); if (state.failed) return result;
            	Match(input,23,FOLLOW_23_in_config300); if (state.failed) return result;
            	PushFollow(FOLLOW_number_in_config318);
            	fptu = number();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,24,FOLLOW_24_in_config332); if (state.failed) return result;
            	PushFollow(FOLLOW_number_in_config336);
            	width = number();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,24,FOLLOW_24_in_config348); if (state.failed) return result;
            	PushFollow(FOLLOW_number_in_config352);
            	height = number();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,25,FOLLOW_25_in_config362); if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = new Config(fptu, width, height); 
            	}
            	Match(input,26,FOLLOW_26_in_config390); if (state.failed) return result;

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
    // Movimentum.g:41:5: thingdefinition returns [Thing result] : IDENT ':' ( FILENAME anchordefinitions[defs] | BAR anchordefinitions[defs] ) ';' ;
    public Thing thingdefinition() // throws RecognitionException [1]
    {   
        Thing result = default(Thing);

        IToken IDENT1 = null;
        IToken FILENAME2 = null;

        try 
    	{
            // Movimentum.g:42:7: ( IDENT ':' ( FILENAME anchordefinitions[defs] | BAR anchordefinitions[defs] ) ';' )
            // Movimentum.g:42:9: IDENT ':' ( FILENAME anchordefinitions[defs] | BAR anchordefinitions[defs] ) ';'
            {
            	IDENT1=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_thingdefinition419); if (state.failed) return result;
            	Match(input,27,FOLLOW_27_in_thingdefinition429); if (state.failed) return result;
            	 var defs = new List<ConstAnchor>(); 
            	// Movimentum.g:44:3: ( FILENAME anchordefinitions[defs] | BAR anchordefinitions[defs] )
            	int alt4 = 2;
            	int LA4_0 = input.LA(1);

            	if ( (LA4_0 == FILENAME) )
            	{
            	    alt4 = 1;
            	}
            	else if ( (LA4_0 == BAR) )
            	{
            	    alt4 = 2;
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            	    NoViableAltException nvae_d4s0 =
            	        new NoViableAltException("", 4, 0, input);

            	    throw nvae_d4s0;
            	}
            	switch (alt4) 
            	{
            	    case 1 :
            	        // Movimentum.g:44:5: FILENAME anchordefinitions[defs]
            	        {
            	        	FILENAME2=(IToken)Match(input,FILENAME,FOLLOW_FILENAME_in_thingdefinition453); if (state.failed) return result;
            	        	PushFollow(FOLLOW_anchordefinitions_in_thingdefinition465);
            	        	anchordefinitions(defs);
            	        	state.followingStackPointer--;
            	        	if (state.failed) return result;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   result = new ImageThing(IDENT1.Text, 
            	        	  							                          ImageFromFile(FILENAME2.Text), 
            	        	  													  defs);
            	        	  		                    
            	        	}

            	        }
            	        break;
            	    case 2 :
            	        // Movimentum.g:50:5: BAR anchordefinitions[defs]
            	        {
            	        	Match(input,BAR,FOLLOW_BAR_in_thingdefinition496); if (state.failed) return result;
            	        	PushFollow(FOLLOW_anchordefinitions_in_thingdefinition508);
            	        	anchordefinitions(defs);
            	        	state.followingStackPointer--;
            	        	if (state.failed) return result;
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   result = new BarThing(IDENT1.Text, defs); 
            	        	}

            	        }
            	        break;

            	}

            	Match(input,26,FOLLOW_26_in_thingdefinition547); if (state.failed) return result;

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


    // $ANTLR start "anchordefinitions"
    // Movimentum.g:57:5: anchordefinitions[List<ConstAnchor> defs] : ( anchordefinition[defs] )+ ;
    public void anchordefinitions(List<ConstAnchor> defs) // throws RecognitionException [1]
    {   
        try 
    	{
            // Movimentum.g:58:4: ( ( anchordefinition[defs] )+ )
            // Movimentum.g:58:6: ( anchordefinition[defs] )+
            {
            	// Movimentum.g:58:6: ( anchordefinition[defs] )+
            	int cnt5 = 0;
            	do 
            	{
            	    int alt5 = 2;
            	    int LA5_0 = input.LA(1);

            	    if ( (LA5_0 == IDENT) )
            	    {
            	        alt5 = 1;
            	    }


            	    switch (alt5) 
            		{
            			case 1 :
            			    // Movimentum.g:58:8: anchordefinition[defs]
            			    {
            			    	PushFollow(FOLLOW_anchordefinition_in_anchordefinitions573);
            			    	anchordefinition(defs);
            			    	state.followingStackPointer--;
            			    	if (state.failed) return ;

            			    }
            			    break;

            			default:
            			    if ( cnt5 >= 1 ) goto loop5;
            			    if ( state.backtracking > 0 ) {state.failed = true; return ;}
            		            EarlyExitException eee =
            		                new EarlyExitException(5, input);
            		            throw eee;
            	    }
            	    cnt5++;
            	} while (true);

            	loop5:
            		;	// Stops C# compiler whinging that label 'loop5' has no statements


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
    // $ANTLR end "anchordefinitions"


    // $ANTLR start "anchordefinition"
    // Movimentum.g:62:5: anchordefinition[List<ConstAnchor> defs] : n= IDENT '=' (c= constvector | i= IDENT '+' c= constvector | i= IDENT '-' c= constvector ) ;
    public void anchordefinition(List<ConstAnchor> defs) // throws RecognitionException [1]
    {   
        IToken n = null;
        IToken i = null;
        ConstVector c = default(ConstVector);


        try 
    	{
            // Movimentum.g:63:7: (n= IDENT '=' (c= constvector | i= IDENT '+' c= constvector | i= IDENT '-' c= constvector ) )
            // Movimentum.g:63:9: n= IDENT '=' (c= constvector | i= IDENT '+' c= constvector | i= IDENT '-' c= constvector )
            {
            	n=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_anchordefinition618); if (state.failed) return ;
            	Match(input,28,FOLLOW_28_in_anchordefinition628); if (state.failed) return ;
            	 ConstVector v = null; 
            	// Movimentum.g:65:9: (c= constvector | i= IDENT '+' c= constvector | i= IDENT '-' c= constvector )
            	int alt6 = 3;
            	int LA6_0 = input.LA(1);

            	if ( (LA6_0 == 31) )
            	{
            	    alt6 = 1;
            	}
            	else if ( (LA6_0 == IDENT) )
            	{
            	    int LA6_2 = input.LA(2);

            	    if ( (LA6_2 == 29) )
            	    {
            	        alt6 = 2;
            	    }
            	    else if ( (LA6_2 == 30) )
            	    {
            	        alt6 = 3;
            	    }
            	    else 
            	    {
            	        if ( state.backtracking > 0 ) {state.failed = true; return ;}
            	        NoViableAltException nvae_d6s2 =
            	            new NoViableAltException("", 6, 2, input);

            	        throw nvae_d6s2;
            	    }
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return ;}
            	    NoViableAltException nvae_d6s0 =
            	        new NoViableAltException("", 6, 0, input);

            	    throw nvae_d6s0;
            	}
            	switch (alt6) 
            	{
            	    case 1 :
            	        // Movimentum.g:65:11: c= constvector
            	        {
            	        	PushFollow(FOLLOW_constvector_in_anchordefinition669);
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
            	        // Movimentum.g:66:11: i= IDENT '+' c= constvector
            	        {
            	        	i=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_anchordefinition698); if (state.failed) return ;
            	        	Match(input,29,FOLLOW_29_in_anchordefinition700); if (state.failed) return ;
            	        	PushFollow(FOLLOW_constvector_in_anchordefinition704);
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
            	        // Movimentum.g:67:11: i= IDENT '-' c= constvector
            	        {
            	        	i=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_anchordefinition721); if (state.failed) return ;
            	        	Match(input,30,FOLLOW_30_in_anchordefinition723); if (state.failed) return ;
            	        	PushFollow(FOLLOW_constvector_in_anchordefinition727);
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
            	   defs.Add(new ConstAnchor(n.Text, v)); 
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
    // Movimentum.g:71:5: constvector returns [ConstVector result] : '[' ( '-' c= constscalar | c= constscalar ) ',' ( '-' c= constscalar | c= constscalar ) ( ',' ( '-' c= constscalar | c= constscalar ) | ) ']' ;
    public ConstVector constvector() // throws RecognitionException [1]
    {   
        ConstVector result = default(ConstVector);

        double c = default(double);


        try 
    	{
            // Movimentum.g:72:7: ( '[' ( '-' c= constscalar | c= constscalar ) ',' ( '-' c= constscalar | c= constscalar ) ( ',' ( '-' c= constscalar | c= constscalar ) | ) ']' )
            // Movimentum.g:72:31: '[' ( '-' c= constscalar | c= constscalar ) ',' ( '-' c= constscalar | c= constscalar ) ( ',' ( '-' c= constscalar | c= constscalar ) | ) ']'
            {
            	 double x = double.NaN, y = double.NaN; 
            	Match(input,31,FOLLOW_31_in_constvector830); if (state.failed) return result;
            	// Movimentum.g:74:9: ( '-' c= constscalar | c= constscalar )
            	int alt7 = 2;
            	int LA7_0 = input.LA(1);

            	if ( (LA7_0 == 30) )
            	{
            	    alt7 = 1;
            	}
            	else if ( (LA7_0 == NUMBER || LA7_0 == 31) )
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
            	        // Movimentum.g:74:10: '-' c= constscalar
            	        {
            	        	Match(input,30,FOLLOW_30_in_constvector841); if (state.failed) return result;
            	        	PushFollow(FOLLOW_constscalar_in_constvector845);
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
            	        // Movimentum.g:75:14: c= constscalar
            	        {
            	        	PushFollow(FOLLOW_constscalar_in_constvector867);
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

            	Match(input,24,FOLLOW_24_in_constvector892); if (state.failed) return result;
            	// Movimentum.g:78:9: ( '-' c= constscalar | c= constscalar )
            	int alt8 = 2;
            	int LA8_0 = input.LA(1);

            	if ( (LA8_0 == 30) )
            	{
            	    alt8 = 1;
            	}
            	else if ( (LA8_0 == NUMBER || LA8_0 == 31) )
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
            	        // Movimentum.g:78:10: '-' c= constscalar
            	        {
            	        	Match(input,30,FOLLOW_30_in_constvector903); if (state.failed) return result;
            	        	PushFollow(FOLLOW_constscalar_in_constvector907);
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
            	        // Movimentum.g:79:14: c= constscalar
            	        {
            	        	PushFollow(FOLLOW_constscalar_in_constvector929);
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

            	// Movimentum.g:81:9: ( ',' ( '-' c= constscalar | c= constscalar ) | )
            	int alt10 = 2;
            	int LA10_0 = input.LA(1);

            	if ( (LA10_0 == 24) )
            	{
            	    alt10 = 1;
            	}
            	else if ( (LA10_0 == 32) )
            	{
            	    alt10 = 2;
            	}
            	else 
            	{
            	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            	    NoViableAltException nvae_d10s0 =
            	        new NoViableAltException("", 10, 0, input);

            	    throw nvae_d10s0;
            	}
            	switch (alt10) 
            	{
            	    case 1 :
            	        // Movimentum.g:81:11: ',' ( '-' c= constscalar | c= constscalar )
            	        {
            	        	Match(input,24,FOLLOW_24_in_constvector956); if (state.failed) return result;
            	        	 double z = double.NaN; 
            	        	// Movimentum.g:82:11: ( '-' c= constscalar | c= constscalar )
            	        	int alt9 = 2;
            	        	int LA9_0 = input.LA(1);

            	        	if ( (LA9_0 == 30) )
            	        	{
            	        	    alt9 = 1;
            	        	}
            	        	else if ( (LA9_0 == NUMBER || LA9_0 == 31) )
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
            	        	        // Movimentum.g:82:12: '-' c= constscalar
            	        	        {
            	        	        	Match(input,30,FOLLOW_30_in_constvector987); if (state.failed) return result;
            	        	        	PushFollow(FOLLOW_constscalar_in_constvector991);
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
            	        	        // Movimentum.g:83:16: c= constscalar
            	        	        {
            	        	        	PushFollow(FOLLOW_constscalar_in_constvector1013);
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
            	        // Movimentum.g:85:25: 
            	        {
            	        	if ( state.backtracking == 0 ) 
            	        	{
            	        	   result = new ConstVector(x,y); 
            	        	}

            	        }
            	        break;

            	}

            	Match(input,32,FOLLOW_32_in_constvector1088); if (state.failed) return result;

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
    // Movimentum.g:90:5: constscalar returns [double result] : (n= number | c= constvector ( X | Y | Z ) );
    public double constscalar() // throws RecognitionException [1]
    {   
        double result = default(double);

        double n = default(double);

        ConstVector c = default(ConstVector);


        try 
    	{
            // Movimentum.g:91:7: (n= number | c= constvector ( X | Y | Z ) )
            int alt12 = 2;
            int LA12_0 = input.LA(1);

            if ( (LA12_0 == NUMBER) )
            {
                alt12 = 1;
            }
            else if ( (LA12_0 == 31) )
            {
                alt12 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d12s0 =
                    new NoViableAltException("", 12, 0, input);

                throw nvae_d12s0;
            }
            switch (alt12) 
            {
                case 1 :
                    // Movimentum.g:91:9: n= number
                    {
                    	PushFollow(FOLLOW_number_in_constscalar1136);
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
                    // Movimentum.g:92:9: c= constvector ( X | Y | Z )
                    {
                    	PushFollow(FOLLOW_constvector_in_constscalar1161);
                    	c = constvector();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	// Movimentum.g:93:9: ( X | Y | Z )
                    	int alt11 = 3;
                    	switch ( input.LA(1) ) 
                    	{
                    	case X:
                    		{
                    	    alt11 = 1;
                    	    }
                    	    break;
                    	case Y:
                    		{
                    	    alt11 = 2;
                    	    }
                    	    break;
                    	case Z:
                    		{
                    	    alt11 = 3;
                    	    }
                    	    break;
                    		default:
                    		    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    		    NoViableAltException nvae_d11s0 =
                    		        new NoViableAltException("", 11, 0, input);

                    		    throw nvae_d11s0;
                    	}

                    	switch (alt11) 
                    	{
                    	    case 1 :
                    	        // Movimentum.g:93:11: X
                    	        {
                    	        	Match(input,X,FOLLOW_X_in_constscalar1180); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = c.X; 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:94:11: Y
                    	        {
                    	        	Match(input,Y,FOLLOW_Y_in_constscalar1210); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = c.Y; 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:95:11: Z
                    	        {
                    	        	Match(input,Z,FOLLOW_Z_in_constscalar1240); if (state.failed) return result;
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
    // Movimentum.g:99:5: time : ( '@' n= number | '@' '+' n= number );
    public void time() // throws RecognitionException [1]
    {   
        double n = default(double);


        try 
    	{
            // Movimentum.g:100:7: ( '@' n= number | '@' '+' n= number )
            int alt13 = 2;
            int LA13_0 = input.LA(1);

            if ( (LA13_0 == 33) )
            {
                int LA13_1 = input.LA(2);

                if ( (LA13_1 == 29) )
                {
                    alt13 = 2;
                }
                else if ( (LA13_1 == NUMBER) )
                {
                    alt13 = 1;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return ;}
                    NoViableAltException nvae_d13s1 =
                        new NoViableAltException("", 13, 1, input);

                    throw nvae_d13s1;
                }
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return ;}
                NoViableAltException nvae_d13s0 =
                    new NoViableAltException("", 13, 0, input);

                throw nvae_d13s0;
            }
            switch (alt13) 
            {
                case 1 :
                    // Movimentum.g:100:9: '@' n= number
                    {
                    	Match(input,33,FOLLOW_33_in_time1293); if (state.failed) return ;
                    	PushFollow(FOLLOW_number_in_time1297);
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
                    // Movimentum.g:101:9: '@' '+' n= number
                    {
                    	Match(input,33,FOLLOW_33_in_time1316); if (state.failed) return ;
                    	Match(input,29,FOLLOW_29_in_time1318); if (state.failed) return ;
                    	PushFollow(FOLLOW_number_in_time1322);
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
    // Movimentum.g:104:5: constraint returns [Constraint result] : (a= anchor '=' v= vectorexpr ';' | i= IDENT '=' s= scalarexpr ';' | i= IDENT ( '<' | '>' | '<=' | '>=' ) s= scalarexpr ';' );
    public Constraint constraint() // throws RecognitionException [1]
    {   
        Constraint result = default(Constraint);

        IToken i = null;
        Anchor a = default(Anchor);

        VectorExpr v = default(VectorExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:105:7: (a= anchor '=' v= vectorexpr ';' | i= IDENT '=' s= scalarexpr ';' | i= IDENT ( '<' | '>' | '<=' | '>=' ) s= scalarexpr ';' )
            int alt15 = 3;
            int LA15_0 = input.LA(1);

            if ( (LA15_0 == IDENT) )
            {
                switch ( input.LA(2) ) 
                {
                case 28:
                	{
                    alt15 = 2;
                    }
                    break;
                case 34:
                case 35:
                case 36:
                case 37:
                	{
                    alt15 = 3;
                    }
                    break;
                case 40:
                	{
                    alt15 = 1;
                    }
                    break;
                	default:
                	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                	    NoViableAltException nvae_d15s1 =
                	        new NoViableAltException("", 15, 1, input);

                	    throw nvae_d15s1;
                }

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
                    // Movimentum.g:105:9: a= anchor '=' v= vectorexpr ';'
                    {
                    	PushFollow(FOLLOW_anchor_in_constraint1358);
                    	a = anchor();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,28,FOLLOW_28_in_constraint1368); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_constraint1380);
                    	v = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,26,FOLLOW_26_in_constraint1390); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new VectorEqualityConstraint(a, v); 
                    	}

                    }
                    break;
                case 2 :
                    // Movimentum.g:109:9: i= IDENT '=' s= scalarexpr ';'
                    {
                    	i=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_constraint1420); if (state.failed) return result;
                    	Match(input,28,FOLLOW_28_in_constraint1430); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_constraint1442);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,26,FOLLOW_26_in_constraint1452); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ScalarEqualityConstraint(i.Text, s); 
                    	}

                    }
                    break;
                case 3 :
                    // Movimentum.g:113:9: i= IDENT ( '<' | '>' | '<=' | '>=' ) s= scalarexpr ';'
                    {
                    	i=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_constraint1482); if (state.failed) return result;
                    	 ScalarInequalityOperator op = null; 
                    	// Movimentum.g:114:9: ( '<' | '>' | '<=' | '>=' )
                    	int alt14 = 4;
                    	switch ( input.LA(1) ) 
                    	{
                    	case 34:
                    		{
                    	    alt14 = 1;
                    	    }
                    	    break;
                    	case 35:
                    		{
                    	    alt14 = 2;
                    	    }
                    	    break;
                    	case 36:
                    		{
                    	    alt14 = 3;
                    	    }
                    	    break;
                    	case 37:
                    		{
                    	    alt14 = 4;
                    	    }
                    	    break;
                    		default:
                    		    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    		    NoViableAltException nvae_d14s0 =
                    		        new NoViableAltException("", 14, 0, input);

                    		    throw nvae_d14s0;
                    	}

                    	switch (alt14) 
                    	{
                    	    case 1 :
                    	        // Movimentum.g:114:11: '<'
                    	        {
                    	        	Match(input,34,FOLLOW_34_in_constraint1508); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.LE; 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:115:11: '>'
                    	        {
                    	        	Match(input,35,FOLLOW_35_in_constraint1536); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.GE; 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:116:11: '<='
                    	        {
                    	        	Match(input,36,FOLLOW_36_in_constraint1564); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.LT; 
                    	        	}

                    	        }
                    	        break;
                    	    case 4 :
                    	        // Movimentum.g:117:11: '>='
                    	        {
                    	        	Match(input,37,FOLLOW_37_in_constraint1591); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   op = ScalarInequalityOperator.GT; 
                    	        	}

                    	        }
                    	        break;

                    	}

                    	PushFollow(FOLLOW_scalarexpr_in_constraint1647);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,26,FOLLOW_26_in_constraint1665); if (state.failed) return result;
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
    // Movimentum.g:123:5: vectorexpr returns [VectorExpr result] options {greedy=true; } : v= vectorexpr2 ( ( '+' | '-' ) v= vectorexpr2 )* ;
    public VectorExpr vectorexpr() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);


        try 
    	{
            // Movimentum.g:125:7: (v= vectorexpr2 ( ( '+' | '-' ) v= vectorexpr2 )* )
            // Movimentum.g:125:9: v= vectorexpr2 ( ( '+' | '-' ) v= vectorexpr2 )*
            {
            	PushFollow(FOLLOW_vectorexpr2_in_vectorexpr1728);
            	v = vectorexpr2();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = v; 
            	}
            	// Movimentum.g:126:9: ( ( '+' | '-' ) v= vectorexpr2 )*
            	do 
            	{
            	    int alt17 = 2;
            	    int LA17_0 = input.LA(1);

            	    if ( ((LA17_0 >= 29 && LA17_0 <= 30)) )
            	    {
            	        alt17 = 1;
            	    }


            	    switch (alt17) 
            		{
            			case 1 :
            			    // Movimentum.g:126:29: ( '+' | '-' ) v= vectorexpr2
            			    {
            			    	 BinaryVectorOperator op = null; 
            			    	// Movimentum.g:127:11: ( '+' | '-' )
            			    	int alt16 = 2;
            			    	int LA16_0 = input.LA(1);

            			    	if ( (LA16_0 == 29) )
            			    	{
            			    	    alt16 = 1;
            			    	}
            			    	else if ( (LA16_0 == 30) )
            			    	{
            			    	    alt16 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            			    	    NoViableAltException nvae_d16s0 =
            			    	        new NoViableAltException("", 16, 0, input);

            			    	    throw nvae_d16s0;
            			    	}
            			    	switch (alt16) 
            			    	{
            			    	    case 1 :
            			    	        // Movimentum.g:127:13: '+'
            			    	        {
            			    	        	Match(input,29,FOLLOW_29_in_vectorexpr1780); if (state.failed) return result;
            			    	        	 op = BinaryVectorOperator.PLUS; 

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // Movimentum.g:128:13: '-'
            			    	        {
            			    	        	Match(input,30,FOLLOW_30_in_vectorexpr1808); if (state.failed) return result;
            			    	        	 op = BinaryVectorOperator.MINUS; 

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_vectorexpr2_in_vectorexpr1865);
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
    // $ANTLR end "vectorexpr"


    // $ANTLR start "vectorexpr2"
    // Movimentum.g:134:5: vectorexpr2 returns [VectorExpr result] : v= vectorexpr3 ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )* ;
    public VectorExpr vectorexpr2() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:135:7: (v= vectorexpr3 ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )* )
            // Movimentum.g:135:9: v= vectorexpr3 ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )*
            {
            	PushFollow(FOLLOW_vectorexpr3_in_vectorexpr21980);
            	v = vectorexpr3();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = v; 
            	}
            	// Movimentum.g:136:9: ( ROTATE '(' s= scalarexpr ')' | '*' s= scalarexpr4 )*
            	do 
            	{
            	    int alt18 = 3;
            	    int LA18_0 = input.LA(1);

            	    if ( (LA18_0 == ROTATE) )
            	    {
            	        alt18 = 1;
            	    }
            	    else if ( (LA18_0 == 38) )
            	    {
            	        alt18 = 2;
            	    }


            	    switch (alt18) 
            		{
            			case 1 :
            			    // Movimentum.g:136:11: ROTATE '(' s= scalarexpr ')'
            			    {
            			    	Match(input,ROTATE,FOLLOW_ROTATE_in_vectorexpr22000); if (state.failed) return result;
            			    	Match(input,23,FOLLOW_23_in_vectorexpr22012); if (state.failed) return result;
            			    	PushFollow(FOLLOW_scalarexpr_in_vectorexpr22026);
            			    	s = scalarexpr();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return result;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	   result = new BinaryScalarVectorExpr(result, BinaryScalarVectorOperator.ROTATE2D, s); 
            			    	}
            			    	Match(input,25,FOLLOW_25_in_vectorexpr22045); if (state.failed) return result;

            			    }
            			    break;
            			case 2 :
            			    // Movimentum.g:140:5: '*' s= scalarexpr4
            			    {
            			    	Match(input,38,FOLLOW_38_in_vectorexpr22051); if (state.failed) return result;
            			    	PushFollow(FOLLOW_scalarexpr4_in_vectorexpr22059);
            			    	s = scalarexpr4();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return result;
            			    	if ( state.backtracking == 0 ) 
            			    	{
            			    	   result = new BinaryScalarVectorExpr(result, BinaryScalarVectorOperator.TIMES, s); 
            			    	}

            			    }
            			    break;

            			default:
            			    goto loop18;
            	    }
            	} while (true);

            	loop18:
            		;	// Stops C# compiler whining that label 'loop18' has no statements


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
    // Movimentum.g:145:5: vectorexpr3 returns [VectorExpr result] : ( '-' v= vectorexpr4 | v= vectorexpr4 );
    public VectorExpr vectorexpr3() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);


        try 
    	{
            // Movimentum.g:146:7: ( '-' v= vectorexpr4 | v= vectorexpr4 )
            int alt19 = 2;
            int LA19_0 = input.LA(1);

            if ( (LA19_0 == 30) )
            {
                alt19 = 1;
            }
            else if ( (LA19_0 == IDENT || (LA19_0 >= INTEGRAL && LA19_0 <= DIFFERENTIAL) || LA19_0 == 23 || LA19_0 == 31 || LA19_0 == 39) )
            {
                alt19 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d19s0 =
                    new NoViableAltException("", 19, 0, input);

                throw nvae_d19s0;
            }
            switch (alt19) 
            {
                case 1 :
                    // Movimentum.g:146:9: '-' v= vectorexpr4
                    {
                    	Match(input,30,FOLLOW_30_in_vectorexpr32105); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr4_in_vectorexpr32109);
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
                    // Movimentum.g:147:9: v= vectorexpr4
                    {
                    	PushFollow(FOLLOW_vectorexpr4_in_vectorexpr32125);
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
    // Movimentum.g:150:5: vectorexpr4 returns [VectorExpr result] : ( INTEGRAL '(' v= vectorexpr ')' | DIFFERENTIAL '(' v= vectorexpr ')' | v= vectorexpr5 );
    public VectorExpr vectorexpr4() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        VectorExpr v = default(VectorExpr);


        try 
    	{
            // Movimentum.g:151:7: ( INTEGRAL '(' v= vectorexpr ')' | DIFFERENTIAL '(' v= vectorexpr ')' | v= vectorexpr5 )
            int alt20 = 3;
            switch ( input.LA(1) ) 
            {
            case INTEGRAL:
            	{
                alt20 = 1;
                }
                break;
            case DIFFERENTIAL:
            	{
                alt20 = 2;
                }
                break;
            case IDENT:
            case 23:
            case 31:
            case 39:
            	{
                alt20 = 3;
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
                    // Movimentum.g:151:9: INTEGRAL '(' v= vectorexpr ')'
                    {
                    	Match(input,INTEGRAL,FOLLOW_INTEGRAL_in_vectorexpr42162); if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_vectorexpr42172); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_vectorexpr42184);
                    	v = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryVectorExpr(UnaryVectorOperator.INTEGRAL, v); 
                    	}
                    	Match(input,25,FOLLOW_25_in_vectorexpr42203); if (state.failed) return result;

                    }
                    break;
                case 2 :
                    // Movimentum.g:155:9: DIFFERENTIAL '(' v= vectorexpr ')'
                    {
                    	Match(input,DIFFERENTIAL,FOLLOW_DIFFERENTIAL_in_vectorexpr42230); if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_vectorexpr42248); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_vectorexpr42277);
                    	v = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryVectorExpr(UnaryVectorOperator.DIFFERENTIAL, v); 
                    	}
                    	Match(input,25,FOLLOW_25_in_vectorexpr42296); if (state.failed) return result;

                    }
                    break;
                case 3 :
                    // Movimentum.g:159:9: v= vectorexpr5
                    {
                    	PushFollow(FOLLOW_vectorexpr5_in_vectorexpr42325);
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
    // Movimentum.g:162:5: vectorexpr5 returns [VectorExpr result] : ( '(' e= vectorexpr ')' | v= vector | a= anchor | IDENT | '_' );
    public VectorExpr vectorexpr5() // throws RecognitionException [1]
    {   
        VectorExpr result = default(VectorExpr);

        IToken IDENT3 = null;
        VectorExpr e = default(VectorExpr);

        Vector v = default(Vector);

        Anchor a = default(Anchor);


        try 
    	{
            // Movimentum.g:163:7: ( '(' e= vectorexpr ')' | v= vector | a= anchor | IDENT | '_' )
            int alt21 = 5;
            switch ( input.LA(1) ) 
            {
            case 23:
            	{
                alt21 = 1;
                }
                break;
            case 31:
            	{
                alt21 = 2;
                }
                break;
            case IDENT:
            	{
                int LA21_3 = input.LA(2);

                if ( ((LA21_3 >= X && LA21_3 <= ROTATE) || LA21_3 == LENGTH || (LA21_3 >= 24 && LA21_3 <= 26) || (LA21_3 >= 29 && LA21_3 <= 30) || LA21_3 == 38) )
                {
                    alt21 = 4;
                }
                else if ( (LA21_3 == 40) )
                {
                    alt21 = 3;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    NoViableAltException nvae_d21s3 =
                        new NoViableAltException("", 21, 3, input);

                    throw nvae_d21s3;
                }
                }
                break;
            case 39:
            	{
                alt21 = 5;
                }
                break;
            	default:
            	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            	    NoViableAltException nvae_d21s0 =
            	        new NoViableAltException("", 21, 0, input);

            	    throw nvae_d21s0;
            }

            switch (alt21) 
            {
                case 1 :
                    // Movimentum.g:163:9: '(' e= vectorexpr ')'
                    {
                    	Match(input,23,FOLLOW_23_in_vectorexpr52362); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_vectorexpr52374);
                    	e = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = e; 
                    	}
                    	Match(input,25,FOLLOW_25_in_vectorexpr52393); if (state.failed) return result;

                    }
                    break;
                case 2 :
                    // Movimentum.g:166:9: v= vector
                    {
                    	PushFollow(FOLLOW_vector_in_vectorexpr52422);
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
                    // Movimentum.g:167:9: a= anchor
                    {
                    	PushFollow(FOLLOW_anchor_in_vectorexpr52447);
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
                    // Movimentum.g:168:9: IDENT
                    {
                    	IDENT3=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_vectorexpr52470); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new VectorVariable(IDENT3.Text); 
                    	}

                    }
                    break;
                case 5 :
                    // Movimentum.g:169:9: '_'
                    {
                    	Match(input,39,FOLLOW_39_in_vectorexpr52496); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new VectorVariable("_#" + _anonymousVarCt++); 
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
    // Movimentum.g:172:5: vector returns [Vector result] : '[' s1= scalarexpr ',' s2= scalarexpr ( ',' s3= scalarexpr )? ']' ;
    public Vector vector() // throws RecognitionException [1]
    {   
        Vector result = default(Vector);

        ScalarExpr s1 = default(ScalarExpr);

        ScalarExpr s2 = default(ScalarExpr);

        ScalarExpr s3 = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:173:7: ( '[' s1= scalarexpr ',' s2= scalarexpr ( ',' s3= scalarexpr )? ']' )
            // Movimentum.g:173:9: '[' s1= scalarexpr ',' s2= scalarexpr ( ',' s3= scalarexpr )? ']'
            {
            	Match(input,31,FOLLOW_31_in_vector2592); if (state.failed) return result;
            	PushFollow(FOLLOW_scalarexpr_in_vector2604);
            	s1 = scalarexpr();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,24,FOLLOW_24_in_vector2614); if (state.failed) return result;
            	PushFollow(FOLLOW_scalarexpr_in_vector2626);
            	s2 = scalarexpr();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	// Movimentum.g:177:9: ( ',' s3= scalarexpr )?
            	int alt22 = 2;
            	int LA22_0 = input.LA(1);

            	if ( (LA22_0 == 24) )
            	{
            	    alt22 = 1;
            	}
            	switch (alt22) 
            	{
            	    case 1 :
            	        // Movimentum.g:177:11: ',' s3= scalarexpr
            	        {
            	        	Match(input,24,FOLLOW_24_in_vector2638); if (state.failed) return result;
            	        	PushFollow(FOLLOW_scalarexpr_in_vector2652);
            	        	s3 = scalarexpr();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return result;

            	        }
            	        break;

            	}

            	Match(input,32,FOLLOW_32_in_vector2667); if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = new Vector(s1, s2, s3 ?? new ConstScalar(0)); 
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
    // Movimentum.g:183:5: anchor returns [Anchor result] : th= thing '.' IDENT ;
    public Anchor anchor() // throws RecognitionException [1]
    {   
        Anchor result = default(Anchor);

        IToken IDENT4 = null;
        string th = default(string);


        try 
    	{
            // Movimentum.g:184:7: (th= thing '.' IDENT )
            // Movimentum.g:184:10: th= thing '.' IDENT
            {
            	PushFollow(FOLLOW_thing_in_anchor2717);
            	th = thing();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	Match(input,40,FOLLOW_40_in_anchor2729); if (state.failed) return result;
            	IDENT4=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_anchor2741); if (state.failed) return result;
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
    // Movimentum.g:189:5: thing returns [string result] : IDENT ;
    public string thing() // throws RecognitionException [1]
    {   
        string result = default(string);

        IToken IDENT5 = null;

        try 
    	{
            // Movimentum.g:190:7: ( IDENT )
            // Movimentum.g:190:9: IDENT
            {
            	IDENT5=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_thing2785); if (state.failed) return result;
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
    // Movimentum.g:193:5: scalarexpr returns [ScalarExpr result] options {greedy=true; } : s= scalarexpr2 ( ( '+' | '-' ) s= scalarexpr2 )* ;
    public ScalarExpr scalarexpr() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:195:7: (s= scalarexpr2 ( ( '+' | '-' ) s= scalarexpr2 )* )
            // Movimentum.g:195:9: s= scalarexpr2 ( ( '+' | '-' ) s= scalarexpr2 )*
            {
            	PushFollow(FOLLOW_scalarexpr2_in_scalarexpr2846);
            	s = scalarexpr2();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = s; 
            	}
            	// Movimentum.g:196:9: ( ( '+' | '-' ) s= scalarexpr2 )*
            	do 
            	{
            	    int alt24 = 2;
            	    int LA24_0 = input.LA(1);

            	    if ( ((LA24_0 >= 29 && LA24_0 <= 30)) )
            	    {
            	        alt24 = 1;
            	    }


            	    switch (alt24) 
            		{
            			case 1 :
            			    // Movimentum.g:196:29: ( '+' | '-' ) s= scalarexpr2
            			    {
            			    	 BinaryScalarOperator op = null; 
            			    	// Movimentum.g:197:11: ( '+' | '-' )
            			    	int alt23 = 2;
            			    	int LA23_0 = input.LA(1);

            			    	if ( (LA23_0 == 29) )
            			    	{
            			    	    alt23 = 1;
            			    	}
            			    	else if ( (LA23_0 == 30) )
            			    	{
            			    	    alt23 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            			    	    NoViableAltException nvae_d23s0 =
            			    	        new NoViableAltException("", 23, 0, input);

            			    	    throw nvae_d23s0;
            			    	}
            			    	switch (alt23) 
            			    	{
            			    	    case 1 :
            			    	        // Movimentum.g:197:13: '+'
            			    	        {
            			    	        	Match(input,29,FOLLOW_29_in_scalarexpr2898); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.PLUS; 

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // Movimentum.g:198:13: '-'
            			    	        {
            			    	        	Match(input,30,FOLLOW_30_in_scalarexpr2926); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.MINUS; 

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_scalarexpr2_in_scalarexpr2983);
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
            			    goto loop24;
            	    }
            	} while (true);

            	loop24:
            		;	// Stops C# compiler whining that label 'loop24' has no statements


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
    // Movimentum.g:204:5: scalarexpr2 returns [ScalarExpr result] options {greedy=true; } : s= scalarexpr3 ( ( '*' | '/' ) s= scalarexpr3 )* ;
    public ScalarExpr scalarexpr2() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:206:7: (s= scalarexpr3 ( ( '*' | '/' ) s= scalarexpr3 )* )
            // Movimentum.g:206:9: s= scalarexpr3 ( ( '*' | '/' ) s= scalarexpr3 )*
            {
            	PushFollow(FOLLOW_scalarexpr3_in_scalarexpr23045);
            	s = scalarexpr3();
            	state.followingStackPointer--;
            	if (state.failed) return result;
            	if ( state.backtracking == 0 ) 
            	{
            	   result = s; 
            	}
            	// Movimentum.g:207:9: ( ( '*' | '/' ) s= scalarexpr3 )*
            	do 
            	{
            	    int alt26 = 2;
            	    int LA26_0 = input.LA(1);

            	    if ( (LA26_0 == 38 || LA26_0 == 41) )
            	    {
            	        alt26 = 1;
            	    }


            	    switch (alt26) 
            		{
            			case 1 :
            			    // Movimentum.g:207:29: ( '*' | '/' ) s= scalarexpr3
            			    {
            			    	 BinaryScalarOperator op = null; 
            			    	// Movimentum.g:208:11: ( '*' | '/' )
            			    	int alt25 = 2;
            			    	int LA25_0 = input.LA(1);

            			    	if ( (LA25_0 == 38) )
            			    	{
            			    	    alt25 = 1;
            			    	}
            			    	else if ( (LA25_0 == 41) )
            			    	{
            			    	    alt25 = 2;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return result;}
            			    	    NoViableAltException nvae_d25s0 =
            			    	        new NoViableAltException("", 25, 0, input);

            			    	    throw nvae_d25s0;
            			    	}
            			    	switch (alt25) 
            			    	{
            			    	    case 1 :
            			    	        // Movimentum.g:208:13: '*'
            			    	        {
            			    	        	Match(input,38,FOLLOW_38_in_scalarexpr23097); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.TIMES; 

            			    	        }
            			    	        break;
            			    	    case 2 :
            			    	        // Movimentum.g:209:13: '/'
            			    	        {
            			    	        	Match(input,41,FOLLOW_41_in_scalarexpr23125); if (state.failed) return result;
            			    	        	 op = BinaryScalarOperator.DIVIDE; 

            			    	        }
            			    	        break;

            			    	}

            			    	PushFollow(FOLLOW_scalarexpr3_in_scalarexpr23182);
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
            			    goto loop26;
            	    }
            	} while (true);

            	loop26:
            		;	// Stops C# compiler whining that label 'loop26' has no statements


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
    // Movimentum.g:215:5: scalarexpr3 returns [ScalarExpr result] : ( '-' s= scalarexpr4 | s= scalarexpr4 );
    public ScalarExpr scalarexpr3() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:216:7: ( '-' s= scalarexpr4 | s= scalarexpr4 )
            int alt27 = 2;
            int LA27_0 = input.LA(1);

            if ( (LA27_0 == 30) )
            {
                alt27 = 1;
            }
            else if ( (LA27_0 == IDENT || (LA27_0 >= INTEGRAL && LA27_0 <= IV) || LA27_0 == NUMBER || LA27_0 == 23 || LA27_0 == 31 || LA27_0 == 39 || LA27_0 == 44) )
            {
                alt27 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d27s0 =
                    new NoViableAltException("", 27, 0, input);

                throw nvae_d27s0;
            }
            switch (alt27) 
            {
                case 1 :
                    // Movimentum.g:216:9: '-' s= scalarexpr4
                    {
                    	Match(input,30,FOLLOW_30_in_scalarexpr33267); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr4_in_scalarexpr33271);
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
                    // Movimentum.g:217:9: s= scalarexpr4
                    {
                    	PushFollow(FOLLOW_scalarexpr4_in_scalarexpr33287);
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
    // Movimentum.g:220:5: scalarexpr4 returns [ScalarExpr result] : (s= scalarexpr5 ( '^2' | '^3' | ) | SQRT '(' s= scalarexpr5 ')' );
    public ScalarExpr scalarexpr4() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:221:4: (s= scalarexpr5 ( '^2' | '^3' | ) | SQRT '(' s= scalarexpr5 ')' )
            int alt29 = 2;
            int LA29_0 = input.LA(1);

            if ( (LA29_0 == IDENT || (LA29_0 >= INTEGRAL && LA29_0 <= DIFFERENTIAL) || (LA29_0 >= ANGLE && LA29_0 <= IV) || LA29_0 == NUMBER || LA29_0 == 23 || LA29_0 == 31 || LA29_0 == 39 || LA29_0 == 44) )
            {
                alt29 = 1;
            }
            else if ( (LA29_0 == SQRT) )
            {
                alt29 = 2;
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
                    // Movimentum.g:221:6: s= scalarexpr5 ( '^2' | '^3' | )
                    {
                    	PushFollow(FOLLOW_scalarexpr5_in_scalarexpr43323);
                    	s = scalarexpr5();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	// Movimentum.g:222:6: ( '^2' | '^3' | )
                    	int alt28 = 3;
                    	switch ( input.LA(1) ) 
                    	{
                    	case 42:
                    		{
                    	    alt28 = 1;
                    	    }
                    	    break;
                    	case 43:
                    		{
                    	    alt28 = 2;
                    	    }
                    	    break;
                    	case ROTATE:
                    	case 24:
                    	case 25:
                    	case 26:
                    	case 27:
                    	case 29:
                    	case 30:
                    	case 32:
                    	case 35:
                    	case 38:
                    	case 41:
                    	case 45:
                    		{
                    	    alt28 = 3;
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
                    	        // Movimentum.g:222:8: '^2'
                    	        {
                    	        	Match(input,42,FOLLOW_42_in_scalarexpr43332); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarExpr(UnaryScalarOperator.SQUARED, s); 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:223:5: '^3'
                    	        {
                    	        	Match(input,43,FOLLOW_43_in_scalarexpr43353); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryScalarExpr(UnaryScalarOperator.CUBED, s); 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:224:23: 
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
                    // Movimentum.g:226:6: SQRT '(' s= scalarexpr5 ')'
                    {
                    	Match(input,SQRT,FOLLOW_SQRT_in_scalarexpr43403); if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_scalarexpr43410); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr5_in_scalarexpr43422);
                    	s = scalarexpr5();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryScalarExpr(UnaryScalarOperator.SQUAREROOT, s); 
                    	}
                    	Match(input,25,FOLLOW_25_in_scalarexpr43437); if (state.failed) return result;

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
    // Movimentum.g:232:5: scalarexpr5 returns [ScalarExpr result] : (s= scalarexpr5Ambiguous | INTEGRAL '(' s= scalarexpr ')' | DIFFERENTIAL '(' s= scalarexpr ')' | ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')' | '{' t= scalarexpr ':' l= scalarexpr ( '>' r= scalarexpr ':' l= scalarexpr )+ '}' | n= number | IDENT | '_' | T | IV );
    public ScalarExpr scalarexpr5() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        IToken IDENT6 = null;
        ScalarExpr s = default(ScalarExpr);

        VectorExpr v1 = default(VectorExpr);

        VectorExpr v2 = default(VectorExpr);

        ScalarExpr t = default(ScalarExpr);

        ScalarExpr l = default(ScalarExpr);

        ScalarExpr r = default(ScalarExpr);

        double n = default(double);


        try 
    	{
            // Movimentum.g:233:7: (s= scalarexpr5Ambiguous | INTEGRAL '(' s= scalarexpr ')' | DIFFERENTIAL '(' s= scalarexpr ')' | ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')' | '{' t= scalarexpr ':' l= scalarexpr ( '>' r= scalarexpr ':' l= scalarexpr )+ '}' | n= number | IDENT | '_' | T | IV )
            int alt31 = 10;
            alt31 = dfa31.Predict(input);
            switch (alt31) 
            {
                case 1 :
                    // Movimentum.g:233:9: s= scalarexpr5Ambiguous
                    {
                    	PushFollow(FOLLOW_scalarexpr5Ambiguous_in_scalarexpr53465);
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
                    // Movimentum.g:235:9: INTEGRAL '(' s= scalarexpr ')'
                    {
                    	Match(input,INTEGRAL,FOLLOW_INTEGRAL_in_scalarexpr53505); if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_scalarexpr53507); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr53529);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,25,FOLLOW_25_in_scalarexpr53547); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryScalarExpr(UnaryScalarOperator.INTEGRAL, s); 
                    	}

                    }
                    break;
                case 3 :
                    // Movimentum.g:238:9: DIFFERENTIAL '(' s= scalarexpr ')'
                    {
                    	Match(input,DIFFERENTIAL,FOLLOW_DIFFERENTIAL_in_scalarexpr53573); if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_scalarexpr53575); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr53593);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,25,FOLLOW_25_in_scalarexpr53611); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new UnaryScalarExpr(UnaryScalarOperator.DIFFERENTIAL, s); 
                    	}

                    }
                    break;
                case 4 :
                    // Movimentum.g:241:9: ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')'
                    {
                    	Match(input,ANGLE,FOLLOW_ANGLE_in_scalarexpr53637); if (state.failed) return result;
                    	Match(input,23,FOLLOW_23_in_scalarexpr53639); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_scalarexpr53664);
                    	v1 = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,24,FOLLOW_24_in_scalarexpr53681); if (state.failed) return result;
                    	PushFollow(FOLLOW_vectorexpr_in_scalarexpr53710);
                    	v2 = vectorexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,25,FOLLOW_25_in_scalarexpr53722); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new BinaryVectorScalarExpr(v1, BinaryVectorScalarOperator.ANGLE, v2); 
                    	}

                    }
                    break;
                case 5 :
                    // Movimentum.g:246:9: '{' t= scalarexpr ':' l= scalarexpr ( '>' r= scalarexpr ':' l= scalarexpr )+ '}'
                    {
                    	Match(input,44,FOLLOW_44_in_scalarexpr53748); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr53758);
                    	t = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	Match(input,27,FOLLOW_27_in_scalarexpr53765); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr53772);
                    	l = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	 var pairs = new List<RangeScalarExpr.Pair>(); 
                    	// Movimentum.g:250:6: ( '>' r= scalarexpr ':' l= scalarexpr )+
                    	int cnt30 = 0;
                    	do 
                    	{
                    	    int alt30 = 2;
                    	    int LA30_0 = input.LA(1);

                    	    if ( (LA30_0 == 35) )
                    	    {
                    	        alt30 = 1;
                    	    }


                    	    switch (alt30) 
                    		{
                    			case 1 :
                    			    // Movimentum.g:250:8: '>' r= scalarexpr ':' l= scalarexpr
                    			    {
                    			    	Match(input,35,FOLLOW_35_in_scalarexpr53790); if (state.failed) return result;
                    			    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr53804);
                    			    	r = scalarexpr();
                    			    	state.followingStackPointer--;
                    			    	if (state.failed) return result;
                    			    	Match(input,27,FOLLOW_27_in_scalarexpr53810); if (state.failed) return result;
                    			    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr53824);
                    			    	l = scalarexpr();
                    			    	state.followingStackPointer--;
                    			    	if (state.failed) return result;
                    			    	if ( state.backtracking == 0 ) 
                    			    	{
                    			    	   pairs.Add(new RangeScalarExpr.Pair(r,l)); 
                    			    	}

                    			    }
                    			    break;

                    			default:
                    			    if ( cnt30 >= 1 ) goto loop30;
                    			    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    		            EarlyExitException eee =
                    		                new EarlyExitException(30, input);
                    		            throw eee;
                    	    }
                    	    cnt30++;
                    	} while (true);

                    	loop30:
                    		;	// Stops C# compiler whinging that label 'loop30' has no statements

                    	Match(input,45,FOLLOW_45_in_scalarexpr53840); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new RangeScalarExpr(t, l, pairs); 
                    	}

                    }
                    break;
                case 6 :
                    // Movimentum.g:256:9: n= number
                    {
                    	PushFollow(FOLLOW_number_in_scalarexpr53870);
                    	n = number();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ConstScalar(n); 
                    	}

                    }
                    break;
                case 7 :
                    // Movimentum.g:257:9: IDENT
                    {
                    	IDENT6=(IToken)Match(input,IDENT,FOLLOW_IDENT_in_scalarexpr53893); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ScalarVariable(IDENT6.Text); 
                    	}

                    }
                    break;
                case 8 :
                    // Movimentum.g:258:9: '_'
                    {
                    	Match(input,39,FOLLOW_39_in_scalarexpr53919); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new ScalarVariable("_$" + _anonymousVarCt++); 
                    	}

                    }
                    break;
                case 9 :
                    // Movimentum.g:260:9: T
                    {
                    	Match(input,T,FOLLOW_T_in_scalarexpr53954); if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = new T(); 
                    	}

                    }
                    break;
                case 10 :
                    // Movimentum.g:261:9: IV
                    {
                    	Match(input,IV,FOLLOW_IV_in_scalarexpr53984); if (state.failed) return result;
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
    // Movimentum.g:264:4: scalarexpr5Ambiguous returns [ScalarExpr result] options {backtrack=true; } : (v= vectorexpr5 ( X | Y | Z | LENGTH ) | '(' s= scalarexpr ')' );
    public ScalarExpr scalarexpr5Ambiguous() // throws RecognitionException [1]
    {   
        ScalarExpr result = default(ScalarExpr);

        VectorExpr v = default(VectorExpr);

        ScalarExpr s = default(ScalarExpr);


        try 
    	{
            // Movimentum.g:266:6: (v= vectorexpr5 ( X | Y | Z | LENGTH ) | '(' s= scalarexpr ')' )
            int alt33 = 2;
            int LA33_0 = input.LA(1);

            if ( (LA33_0 == 23) )
            {
                int LA33_1 = input.LA(2);

                if ( (synpred1_Movimentum()) )
                {
                    alt33 = 1;
                }
                else if ( (true) )
                {
                    alt33 = 2;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    NoViableAltException nvae_d33s1 =
                        new NoViableAltException("", 33, 1, input);

                    throw nvae_d33s1;
                }
            }
            else if ( (LA33_0 == IDENT || LA33_0 == 31 || LA33_0 == 39) )
            {
                alt33 = 1;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return result;}
                NoViableAltException nvae_d33s0 =
                    new NoViableAltException("", 33, 0, input);

                throw nvae_d33s0;
            }
            switch (alt33) 
            {
                case 1 :
                    // Movimentum.g:266:9: v= vectorexpr5 ( X | Y | Z | LENGTH )
                    {
                    	PushFollow(FOLLOW_vectorexpr5_in_scalarexpr5Ambiguous4085);
                    	v = vectorexpr5();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	// Movimentum.g:267:8: ( X | Y | Z | LENGTH )
                    	int alt32 = 4;
                    	switch ( input.LA(1) ) 
                    	{
                    	case X:
                    		{
                    	    alt32 = 1;
                    	    }
                    	    break;
                    	case Y:
                    		{
                    	    alt32 = 2;
                    	    }
                    	    break;
                    	case Z:
                    		{
                    	    alt32 = 3;
                    	    }
                    	    break;
                    	case LENGTH:
                    		{
                    	    alt32 = 4;
                    	    }
                    	    break;
                    		default:
                    		    if ( state.backtracking > 0 ) {state.failed = true; return result;}
                    		    NoViableAltException nvae_d32s0 =
                    		        new NoViableAltException("", 32, 0, input);

                    		    throw nvae_d32s0;
                    	}

                    	switch (alt32) 
                    	{
                    	    case 1 :
                    	        // Movimentum.g:267:10: X
                    	        {
                    	        	Match(input,X,FOLLOW_X_in_scalarexpr5Ambiguous4096); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryVectorScalarExpr(v, UnaryVectorScalarOperator.X); 
                    	        	}

                    	        }
                    	        break;
                    	    case 2 :
                    	        // Movimentum.g:268:10: Y
                    	        {
                    	        	Match(input,Y,FOLLOW_Y_in_scalarexpr5Ambiguous4126); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryVectorScalarExpr(v, UnaryVectorScalarOperator.Y); 
                    	        	}

                    	        }
                    	        break;
                    	    case 3 :
                    	        // Movimentum.g:269:10: Z
                    	        {
                    	        	Match(input,Z,FOLLOW_Z_in_scalarexpr5Ambiguous4156); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryVectorScalarExpr(v, UnaryVectorScalarOperator.Z); 
                    	        	}

                    	        }
                    	        break;
                    	    case 4 :
                    	        // Movimentum.g:270:10: LENGTH
                    	        {
                    	        	Match(input,LENGTH,FOLLOW_LENGTH_in_scalarexpr5Ambiguous4186); if (state.failed) return result;
                    	        	if ( state.backtracking == 0 ) 
                    	        	{
                    	        	   result = new UnaryVectorScalarExpr(v, UnaryVectorScalarOperator.LENGTH); 
                    	        	}

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // Movimentum.g:272:8: '(' s= scalarexpr ')'
                    {
                    	Match(input,23,FOLLOW_23_in_scalarexpr5Ambiguous4234); if (state.failed) return result;
                    	PushFollow(FOLLOW_scalarexpr_in_scalarexpr5Ambiguous4259);
                    	s = scalarexpr();
                    	state.followingStackPointer--;
                    	if (state.failed) return result;
                    	if ( state.backtracking == 0 ) 
                    	{
                    	   result = s; 
                    	}
                    	Match(input,25,FOLLOW_25_in_scalarexpr5Ambiguous4278); if (state.failed) return result;

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
    // Movimentum.g:277:4: number returns [double value] : NUMBER ;
    public double number() // throws RecognitionException [1]
    {   
        double value = default(double);

        IToken NUMBER7 = null;

        try 
    	{
            // Movimentum.g:278:6: ( NUMBER )
            // Movimentum.g:278:8: NUMBER
            {
            	NUMBER7=(IToken)Match(input,NUMBER,FOLLOW_NUMBER_in_number4304); if (state.failed) return value;
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


        // Movimentum.g:266:9: (v= vectorexpr5 ( X | Y | Z | LENGTH ) )
        // Movimentum.g:266:9: v= vectorexpr5 ( X | Y | Z | LENGTH )
        {
        	PushFollow(FOLLOW_vectorexpr5_in_synpred1_Movimentum4085);
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


   	protected DFA31 dfa31;
	private void InitializeCyclicDFAs()
	{
    	this.dfa31 = new DFA31(this);
	}

    const string DFA31_eotS =
        "\x0d\uffff";
    const string DFA31_eofS =
        "\x0d\uffff";
    const string DFA31_minS =
        "\x01\x05\x01\uffff\x02\x08\x09\uffff";
    const string DFA31_maxS =
        "\x01\x2c\x01\uffff\x02\x2d\x09\uffff";
    const string DFA31_acceptS =
        "\x01\uffff\x01\x01\x02\uffff\x01\x02\x01\x03\x01\x04\x01\x05\x01"+
        "\x06\x01\x09\x01\x0a\x01\x07\x01\x08";
    const string DFA31_specialS =
        "\x0d\uffff}>";
    static readonly string[] DFA31_transitionS = {
            "\x01\x02\x06\uffff\x01\x04\x01\x05\x01\uffff\x01\x06\x01\x09"+
            "\x01\x0a\x01\uffff\x01\x08\x03\uffff\x01\x01\x07\uffff\x01\x01"+
            "\x07\uffff\x01\x03\x04\uffff\x01\x07",
            "",
            "\x03\x01\x01\x0b\x06\uffff\x01\x01\x05\uffff\x04\x0b\x01\uffff"+
            "\x02\x0b\x01\uffff\x01\x0b\x02\uffff\x01\x0b\x02\uffff\x01\x0b"+
            "\x01\uffff\x01\x01\x03\x0b\x01\uffff\x01\x0b",
            "\x03\x01\x01\x0c\x06\uffff\x01\x01\x05\uffff\x04\x0c\x01\uffff"+
            "\x02\x0c\x01\uffff\x01\x0c\x02\uffff\x01\x0c\x02\uffff\x01\x0c"+
            "\x02\uffff\x03\x0c\x01\uffff\x01\x0c",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA31_eot = DFA.UnpackEncodedString(DFA31_eotS);
    static readonly short[] DFA31_eof = DFA.UnpackEncodedString(DFA31_eofS);
    static readonly char[] DFA31_min = DFA.UnpackEncodedStringToUnsignedChars(DFA31_minS);
    static readonly char[] DFA31_max = DFA.UnpackEncodedStringToUnsignedChars(DFA31_maxS);
    static readonly short[] DFA31_accept = DFA.UnpackEncodedString(DFA31_acceptS);
    static readonly short[] DFA31_special = DFA.UnpackEncodedString(DFA31_specialS);
    static readonly short[][] DFA31_transition = DFA.UnpackEncodedStringArray(DFA31_transitionS);

    protected class DFA31 : DFA
    {
        public DFA31(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 31;
            this.eot = DFA31_eot;
            this.eof = DFA31_eof;
            this.min = DFA31_min;
            this.max = DFA31_max;
            this.accept = DFA31_accept;
            this.special = DFA31_special;
            this.transition = DFA31_transition;

        }

        override public string Description
        {
            get { return "232:5: scalarexpr5 returns [ScalarExpr result] : (s= scalarexpr5Ambiguous | INTEGRAL '(' s= scalarexpr ')' | DIFFERENTIAL '(' s= scalarexpr ')' | ANGLE '(' v1= vectorexpr ',' v2= vectorexpr ')' | '{' t= scalarexpr ':' l= scalarexpr ( '>' r= scalarexpr ':' l= scalarexpr )+ '}' | n= number | IDENT | '_' | T | IV );"; }
        }

    }

 

    public static readonly BitSet FOLLOW_config_in_script124 = new BitSet(new ulong[]{0x0000000200000020UL});
    public static readonly BitSet FOLLOW_thingdefinition_in_script138 = new BitSet(new ulong[]{0x0000000200000020UL});
    public static readonly BitSet FOLLOW_time_in_script163 = new BitSet(new ulong[]{0x0000000200000020UL});
    public static readonly BitSet FOLLOW_constraint_in_script194 = new BitSet(new ulong[]{0x0000000200000020UL});
    public static readonly BitSet FOLLOW_EOF_in_script269 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CONFIG_in_config298 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_config300 = new BitSet(new ulong[]{0x0000000000080000UL});
    public static readonly BitSet FOLLOW_number_in_config318 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_24_in_config332 = new BitSet(new ulong[]{0x0000000000080000UL});
    public static readonly BitSet FOLLOW_number_in_config336 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_24_in_config348 = new BitSet(new ulong[]{0x0000000000080000UL});
    public static readonly BitSet FOLLOW_number_in_config352 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_config362 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_26_in_config390 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_thingdefinition419 = new BitSet(new ulong[]{0x0000000008000000UL});
    public static readonly BitSet FOLLOW_27_in_thingdefinition429 = new BitSet(new ulong[]{0x00000000000000C0UL});
    public static readonly BitSet FOLLOW_FILENAME_in_thingdefinition453 = new BitSet(new ulong[]{0x0000000000000020UL});
    public static readonly BitSet FOLLOW_anchordefinitions_in_thingdefinition465 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_BAR_in_thingdefinition496 = new BitSet(new ulong[]{0x0000000000000020UL});
    public static readonly BitSet FOLLOW_anchordefinitions_in_thingdefinition508 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_26_in_thingdefinition547 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_anchordefinition_in_anchordefinitions573 = new BitSet(new ulong[]{0x0000000000000022UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchordefinition618 = new BitSet(new ulong[]{0x0000000010000000UL});
    public static readonly BitSet FOLLOW_28_in_anchordefinition628 = new BitSet(new ulong[]{0x0000000080000020UL});
    public static readonly BitSet FOLLOW_constvector_in_anchordefinition669 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchordefinition698 = new BitSet(new ulong[]{0x0000000020000000UL});
    public static readonly BitSet FOLLOW_29_in_anchordefinition700 = new BitSet(new ulong[]{0x0000000080000000UL});
    public static readonly BitSet FOLLOW_constvector_in_anchordefinition704 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchordefinition721 = new BitSet(new ulong[]{0x0000000040000000UL});
    public static readonly BitSet FOLLOW_30_in_anchordefinition723 = new BitSet(new ulong[]{0x0000000080000000UL});
    public static readonly BitSet FOLLOW_constvector_in_anchordefinition727 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_31_in_constvector830 = new BitSet(new ulong[]{0x00000000C0080000UL});
    public static readonly BitSet FOLLOW_30_in_constvector841 = new BitSet(new ulong[]{0x00000000C0080000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector845 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector867 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_24_in_constvector892 = new BitSet(new ulong[]{0x00000000C0080000UL});
    public static readonly BitSet FOLLOW_30_in_constvector903 = new BitSet(new ulong[]{0x00000000C0080000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector907 = new BitSet(new ulong[]{0x0000000101000000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector929 = new BitSet(new ulong[]{0x0000000101000000UL});
    public static readonly BitSet FOLLOW_24_in_constvector956 = new BitSet(new ulong[]{0x00000000C0080000UL});
    public static readonly BitSet FOLLOW_30_in_constvector987 = new BitSet(new ulong[]{0x00000000C0080000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector991 = new BitSet(new ulong[]{0x0000000100000000UL});
    public static readonly BitSet FOLLOW_constscalar_in_constvector1013 = new BitSet(new ulong[]{0x0000000100000000UL});
    public static readonly BitSet FOLLOW_32_in_constvector1088 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_number_in_constscalar1136 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_constvector_in_constscalar1161 = new BitSet(new ulong[]{0x0000000000000700UL});
    public static readonly BitSet FOLLOW_X_in_constscalar1180 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Y_in_constscalar1210 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Z_in_constscalar1240 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_33_in_time1293 = new BitSet(new ulong[]{0x0000000000080000UL});
    public static readonly BitSet FOLLOW_number_in_time1297 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_33_in_time1316 = new BitSet(new ulong[]{0x0000000020000000UL});
    public static readonly BitSet FOLLOW_29_in_time1318 = new BitSet(new ulong[]{0x0000000000080000UL});
    public static readonly BitSet FOLLOW_number_in_time1322 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_anchor_in_constraint1358 = new BitSet(new ulong[]{0x0000000010000000UL});
    public static readonly BitSet FOLLOW_28_in_constraint1368 = new BitSet(new ulong[]{0x00000080C0803020UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_constraint1380 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_26_in_constraint1390 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_constraint1420 = new BitSet(new ulong[]{0x0000000010000000UL});
    public static readonly BitSet FOLLOW_28_in_constraint1430 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_constraint1442 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_26_in_constraint1452 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_constraint1482 = new BitSet(new ulong[]{0x0000003C00000000UL});
    public static readonly BitSet FOLLOW_34_in_constraint1508 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_35_in_constraint1536 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_36_in_constraint1564 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_37_in_constraint1591 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_constraint1647 = new BitSet(new ulong[]{0x0000000004000000UL});
    public static readonly BitSet FOLLOW_26_in_constraint1665 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr2_in_vectorexpr1728 = new BitSet(new ulong[]{0x0000000060000002UL});
    public static readonly BitSet FOLLOW_29_in_vectorexpr1780 = new BitSet(new ulong[]{0x00000080C0803020UL});
    public static readonly BitSet FOLLOW_30_in_vectorexpr1808 = new BitSet(new ulong[]{0x00000080C0803020UL});
    public static readonly BitSet FOLLOW_vectorexpr2_in_vectorexpr1865 = new BitSet(new ulong[]{0x0000000060000002UL});
    public static readonly BitSet FOLLOW_vectorexpr3_in_vectorexpr21980 = new BitSet(new ulong[]{0x0000004000000802UL});
    public static readonly BitSet FOLLOW_ROTATE_in_vectorexpr22000 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_vectorexpr22012 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vectorexpr22026 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_vectorexpr22045 = new BitSet(new ulong[]{0x0000004000000802UL});
    public static readonly BitSet FOLLOW_38_in_vectorexpr22051 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr4_in_vectorexpr22059 = new BitSet(new ulong[]{0x0000004000000802UL});
    public static readonly BitSet FOLLOW_30_in_vectorexpr32105 = new BitSet(new ulong[]{0x00000080C0803020UL});
    public static readonly BitSet FOLLOW_vectorexpr4_in_vectorexpr32109 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr4_in_vectorexpr32125 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTEGRAL_in_vectorexpr42162 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_vectorexpr42172 = new BitSet(new ulong[]{0x00000080C0803020UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_vectorexpr42184 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_vectorexpr42203 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DIFFERENTIAL_in_vectorexpr42230 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_vectorexpr42248 = new BitSet(new ulong[]{0x00000080C0803020UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_vectorexpr42277 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_vectorexpr42296 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr5_in_vectorexpr42325 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_23_in_vectorexpr52362 = new BitSet(new ulong[]{0x00000080C0803020UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_vectorexpr52374 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_vectorexpr52393 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vector_in_vectorexpr52422 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_anchor_in_vectorexpr52447 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_vectorexpr52470 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_39_in_vectorexpr52496 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_31_in_vector2592 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vector2604 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_24_in_vector2614 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vector2626 = new BitSet(new ulong[]{0x0000000101000000UL});
    public static readonly BitSet FOLLOW_24_in_vector2638 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_vector2652 = new BitSet(new ulong[]{0x0000000100000000UL});
    public static readonly BitSet FOLLOW_32_in_vector2667 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_thing_in_anchor2717 = new BitSet(new ulong[]{0x0000010000000000UL});
    public static readonly BitSet FOLLOW_40_in_anchor2729 = new BitSet(new ulong[]{0x0000000000000020UL});
    public static readonly BitSet FOLLOW_IDENT_in_anchor2741 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_thing2785 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr2_in_scalarexpr2846 = new BitSet(new ulong[]{0x0000000060000002UL});
    public static readonly BitSet FOLLOW_29_in_scalarexpr2898 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_30_in_scalarexpr2926 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr2_in_scalarexpr2983 = new BitSet(new ulong[]{0x0000000060000002UL});
    public static readonly BitSet FOLLOW_scalarexpr3_in_scalarexpr23045 = new BitSet(new ulong[]{0x0000024000000002UL});
    public static readonly BitSet FOLLOW_38_in_scalarexpr23097 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_41_in_scalarexpr23125 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr3_in_scalarexpr23182 = new BitSet(new ulong[]{0x0000024000000002UL});
    public static readonly BitSet FOLLOW_30_in_scalarexpr33267 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr4_in_scalarexpr33271 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr4_in_scalarexpr33287 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr5_in_scalarexpr43323 = new BitSet(new ulong[]{0x00000C0000000002UL});
    public static readonly BitSet FOLLOW_42_in_scalarexpr43332 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_43_in_scalarexpr43353 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SQRT_in_scalarexpr43403 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_scalarexpr43410 = new BitSet(new ulong[]{0x00001080C08BB020UL});
    public static readonly BitSet FOLLOW_scalarexpr5_in_scalarexpr43422 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_scalarexpr43437 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_scalarexpr5Ambiguous_in_scalarexpr53465 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_INTEGRAL_in_scalarexpr53505 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_scalarexpr53507 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr53529 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_scalarexpr53547 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_DIFFERENTIAL_in_scalarexpr53573 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_scalarexpr53575 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr53593 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_scalarexpr53611 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_ANGLE_in_scalarexpr53637 = new BitSet(new ulong[]{0x0000000000800000UL});
    public static readonly BitSet FOLLOW_23_in_scalarexpr53639 = new BitSet(new ulong[]{0x00000080C0803020UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_scalarexpr53664 = new BitSet(new ulong[]{0x0000000001000000UL});
    public static readonly BitSet FOLLOW_24_in_scalarexpr53681 = new BitSet(new ulong[]{0x00000080C0803020UL});
    public static readonly BitSet FOLLOW_vectorexpr_in_scalarexpr53710 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_scalarexpr53722 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_44_in_scalarexpr53748 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr53758 = new BitSet(new ulong[]{0x0000000008000000UL});
    public static readonly BitSet FOLLOW_27_in_scalarexpr53765 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr53772 = new BitSet(new ulong[]{0x0000000800000000UL});
    public static readonly BitSet FOLLOW_35_in_scalarexpr53790 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr53804 = new BitSet(new ulong[]{0x0000000008000000UL});
    public static readonly BitSet FOLLOW_27_in_scalarexpr53810 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr53824 = new BitSet(new ulong[]{0x0000200800000000UL});
    public static readonly BitSet FOLLOW_45_in_scalarexpr53840 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_number_in_scalarexpr53870 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IDENT_in_scalarexpr53893 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_39_in_scalarexpr53919 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_T_in_scalarexpr53954 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_IV_in_scalarexpr53984 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr5_in_scalarexpr5Ambiguous4085 = new BitSet(new ulong[]{0x0000000000040700UL});
    public static readonly BitSet FOLLOW_X_in_scalarexpr5Ambiguous4096 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Y_in_scalarexpr5Ambiguous4126 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Z_in_scalarexpr5Ambiguous4156 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LENGTH_in_scalarexpr5Ambiguous4186 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_23_in_scalarexpr5Ambiguous4234 = new BitSet(new ulong[]{0x00001080C08BF020UL});
    public static readonly BitSet FOLLOW_scalarexpr_in_scalarexpr5Ambiguous4259 = new BitSet(new ulong[]{0x0000000002000000UL});
    public static readonly BitSet FOLLOW_25_in_scalarexpr5Ambiguous4278 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NUMBER_in_number4304 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vectorexpr5_in_synpred1_Movimentum4085 = new BitSet(new ulong[]{0x0000000000040700UL});
    public static readonly BitSet FOLLOW_set_in_synpred1_Movimentum4094 = new BitSet(new ulong[]{0x0000000000000002UL});

}
}