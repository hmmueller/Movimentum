// $ANTLR 3.1.1 Movimentum.g 2012-04-28 21:31:50
// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162
namespace  Movimentum.Lexer 
{

using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public partial class MovimentumLexer : Lexer {
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

    public MovimentumLexer() 
    {
		InitializeCyclicDFAs();
    }
    public MovimentumLexer(ICharStream input)
		: this(input, null) {
    }
    public MovimentumLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state) {
		InitializeCyclicDFAs(); 

    }
    
    override public string GrammarFileName
    {
    	get { return "Movimentum.g";} 
    }

    // $ANTLR start "T__23"
    public void mT__23() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__23;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:9:7: ( '(' )
            // Movimentum.g:9:9: '('
            {
            	Match('('); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__23"

    // $ANTLR start "T__24"
    public void mT__24() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__24;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:10:7: ( ',' )
            // Movimentum.g:10:9: ','
            {
            	Match(','); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__24"

    // $ANTLR start "T__25"
    public void mT__25() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__25;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:11:7: ( ')' )
            // Movimentum.g:11:9: ')'
            {
            	Match(')'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__25"

    // $ANTLR start "T__26"
    public void mT__26() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__26;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:12:7: ( ';' )
            // Movimentum.g:12:9: ';'
            {
            	Match(';'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__26"

    // $ANTLR start "T__27"
    public void mT__27() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__27;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:13:7: ( ':' )
            // Movimentum.g:13:9: ':'
            {
            	Match(':'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__27"

    // $ANTLR start "T__28"
    public void mT__28() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__28;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:14:7: ( '=' )
            // Movimentum.g:14:9: '='
            {
            	Match('='); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__28"

    // $ANTLR start "T__29"
    public void mT__29() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__29;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:15:7: ( '+' )
            // Movimentum.g:15:9: '+'
            {
            	Match('+'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__29"

    // $ANTLR start "T__30"
    public void mT__30() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__30;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:16:7: ( '-' )
            // Movimentum.g:16:9: '-'
            {
            	Match('-'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__30"

    // $ANTLR start "T__31"
    public void mT__31() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__31;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:17:7: ( '[' )
            // Movimentum.g:17:9: '['
            {
            	Match('['); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__31"

    // $ANTLR start "T__32"
    public void mT__32() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__32;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:18:7: ( ']' )
            // Movimentum.g:18:9: ']'
            {
            	Match(']'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__32"

    // $ANTLR start "T__33"
    public void mT__33() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__33;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:19:7: ( '@' )
            // Movimentum.g:19:9: '@'
            {
            	Match('@'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__33"

    // $ANTLR start "T__34"
    public void mT__34() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__34;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:20:7: ( '<' )
            // Movimentum.g:20:9: '<'
            {
            	Match('<'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__34"

    // $ANTLR start "T__35"
    public void mT__35() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__35;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:21:7: ( '>' )
            // Movimentum.g:21:9: '>'
            {
            	Match('>'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__35"

    // $ANTLR start "T__36"
    public void mT__36() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__36;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:22:7: ( '<=' )
            // Movimentum.g:22:9: '<='
            {
            	Match("<="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__36"

    // $ANTLR start "T__37"
    public void mT__37() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__37;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:23:7: ( '>=' )
            // Movimentum.g:23:9: '>='
            {
            	Match(">="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__37"

    // $ANTLR start "T__38"
    public void mT__38() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__38;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:24:7: ( '*' )
            // Movimentum.g:24:9: '*'
            {
            	Match('*'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__38"

    // $ANTLR start "T__39"
    public void mT__39() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__39;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:25:7: ( '_' )
            // Movimentum.g:25:9: '_'
            {
            	Match('_'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__39"

    // $ANTLR start "T__40"
    public void mT__40() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__40;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:26:7: ( '.' )
            // Movimentum.g:26:9: '.'
            {
            	Match('.'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__40"

    // $ANTLR start "T__41"
    public void mT__41() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__41;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:27:7: ( '/' )
            // Movimentum.g:27:9: '/'
            {
            	Match('/'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__41"

    // $ANTLR start "T__42"
    public void mT__42() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__42;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:28:7: ( '^2' )
            // Movimentum.g:28:9: '^2'
            {
            	Match("^2"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__42"

    // $ANTLR start "T__43"
    public void mT__43() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__43;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:29:7: ( '^3' )
            // Movimentum.g:29:9: '^3'
            {
            	Match("^3"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__43"

    // $ANTLR start "T__44"
    public void mT__44() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__44;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:30:7: ( '{' )
            // Movimentum.g:30:9: '{'
            {
            	Match('{'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__44"

    // $ANTLR start "T__45"
    public void mT__45() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__45;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:31:7: ( '}' )
            // Movimentum.g:31:9: '}'
            {
            	Match('}'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T__45"

    // $ANTLR start "CONFIG"
    public void mCONFIG() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CONFIG;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:283:8: ( '.config' )
            // Movimentum.g:283:10: '.config'
            {
            	Match(".config"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CONFIG"

    // $ANTLR start "BAR"
    public void mBAR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BAR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:285:5: ( '.bar' )
            // Movimentum.g:285:7: '.bar'
            {
            	Match(".bar"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BAR"

    // $ANTLR start "X"
    public void mX() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = X;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:287:5: ( '.x' )
            // Movimentum.g:287:8: '.x'
            {
            	Match(".x"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "X"

    // $ANTLR start "Y"
    public void mY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = Y;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:288:5: ( '.y' )
            // Movimentum.g:288:8: '.y'
            {
            	Match(".y"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "Y"

    // $ANTLR start "Z"
    public void mZ() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = Z;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:289:5: ( '.z' )
            // Movimentum.g:289:8: '.z'
            {
            	Match(".z"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "Z"

    // $ANTLR start "T"
    public void mT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:290:5: ( '.t' )
            // Movimentum.g:290:8: '.t'
            {
            	Match(".t"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "T"

    // $ANTLR start "IV"
    public void mIV() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IV;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:291:5: ( '.iv' )
            // Movimentum.g:291:8: '.iv'
            {
            	Match(".iv"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IV"

    // $ANTLR start "SQRT"
    public void mSQRT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SQRT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:293:14: ( '.q' | '.sqrt' )
            int alt1 = 2;
            int LA1_0 = input.LA(1);

            if ( (LA1_0 == '.') )
            {
                int LA1_1 = input.LA(2);

                if ( (LA1_1 == 'q') )
                {
                    alt1 = 1;
                }
                else if ( (LA1_1 == 's') )
                {
                    alt1 = 2;
                }
                else 
                {
                    NoViableAltException nvae_d1s1 =
                        new NoViableAltException("", 1, 1, input);

                    throw nvae_d1s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d1s0 =
                    new NoViableAltException("", 1, 0, input);

                throw nvae_d1s0;
            }
            switch (alt1) 
            {
                case 1 :
                    // Movimentum.g:293:16: '.q'
                    {
                    	Match(".q"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:293:23: '.sqrt'
                    {
                    	Match(".sqrt"); 


                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SQRT"

    // $ANTLR start "ROTATE"
    public void mROTATE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ROTATE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:294:14: ( '.r' | '.rotate' )
            int alt2 = 2;
            int LA2_0 = input.LA(1);

            if ( (LA2_0 == '.') )
            {
                int LA2_1 = input.LA(2);

                if ( (LA2_1 == 'r') )
                {
                    int LA2_2 = input.LA(3);

                    if ( (LA2_2 == 'o') )
                    {
                        alt2 = 2;
                    }
                    else 
                    {
                        alt2 = 1;}
                }
                else 
                {
                    NoViableAltException nvae_d2s1 =
                        new NoViableAltException("", 2, 1, input);

                    throw nvae_d2s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d2s0 =
                    new NoViableAltException("", 2, 0, input);

                throw nvae_d2s0;
            }
            switch (alt2) 
            {
                case 1 :
                    // Movimentum.g:294:16: '.r'
                    {
                    	Match(".r"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:294:23: '.rotate'
                    {
                    	Match(".rotate"); 


                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ROTATE"

    // $ANTLR start "INTEGRAL"
    public void mINTEGRAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INTEGRAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:295:14: ( '.i' | '.integral' )
            int alt3 = 2;
            int LA3_0 = input.LA(1);

            if ( (LA3_0 == '.') )
            {
                int LA3_1 = input.LA(2);

                if ( (LA3_1 == 'i') )
                {
                    int LA3_2 = input.LA(3);

                    if ( (LA3_2 == 'n') )
                    {
                        alt3 = 2;
                    }
                    else 
                    {
                        alt3 = 1;}
                }
                else 
                {
                    NoViableAltException nvae_d3s1 =
                        new NoViableAltException("", 3, 1, input);

                    throw nvae_d3s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d3s0 =
                    new NoViableAltException("", 3, 0, input);

                throw nvae_d3s0;
            }
            switch (alt3) 
            {
                case 1 :
                    // Movimentum.g:295:16: '.i'
                    {
                    	Match(".i"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:295:23: '.integral'
                    {
                    	Match(".integral"); 


                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INTEGRAL"

    // $ANTLR start "DIFFERENTIAL"
    public void mDIFFERENTIAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DIFFERENTIAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:296:14: ( '.d' | '.differential' )
            int alt4 = 2;
            int LA4_0 = input.LA(1);

            if ( (LA4_0 == '.') )
            {
                int LA4_1 = input.LA(2);

                if ( (LA4_1 == 'd') )
                {
                    int LA4_2 = input.LA(3);

                    if ( (LA4_2 == 'i') )
                    {
                        alt4 = 2;
                    }
                    else 
                    {
                        alt4 = 1;}
                }
                else 
                {
                    NoViableAltException nvae_d4s1 =
                        new NoViableAltException("", 4, 1, input);

                    throw nvae_d4s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d4s0 =
                    new NoViableAltException("", 4, 0, input);

                throw nvae_d4s0;
            }
            switch (alt4) 
            {
                case 1 :
                    // Movimentum.g:296:16: '.d'
                    {
                    	Match(".d"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:296:23: '.differential'
                    {
                    	Match(".differential"); 


                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DIFFERENTIAL"

    // $ANTLR start "ANGLE"
    public void mANGLE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ANGLE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:297:14: ( '.a' | '.angle' )
            int alt5 = 2;
            int LA5_0 = input.LA(1);

            if ( (LA5_0 == '.') )
            {
                int LA5_1 = input.LA(2);

                if ( (LA5_1 == 'a') )
                {
                    int LA5_2 = input.LA(3);

                    if ( (LA5_2 == 'n') )
                    {
                        alt5 = 2;
                    }
                    else 
                    {
                        alt5 = 1;}
                }
                else 
                {
                    NoViableAltException nvae_d5s1 =
                        new NoViableAltException("", 5, 1, input);

                    throw nvae_d5s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d5s0 =
                    new NoViableAltException("", 5, 0, input);

                throw nvae_d5s0;
            }
            switch (alt5) 
            {
                case 1 :
                    // Movimentum.g:297:16: '.a'
                    {
                    	Match(".a"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:297:23: '.angle'
                    {
                    	Match(".angle"); 


                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ANGLE"

    // $ANTLR start "COLOR"
    public void mCOLOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COLOR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:298:14: ( '.c' | '.color' )
            int alt6 = 2;
            int LA6_0 = input.LA(1);

            if ( (LA6_0 == '.') )
            {
                int LA6_1 = input.LA(2);

                if ( (LA6_1 == 'c') )
                {
                    int LA6_2 = input.LA(3);

                    if ( (LA6_2 == 'o') )
                    {
                        alt6 = 2;
                    }
                    else 
                    {
                        alt6 = 1;}
                }
                else 
                {
                    NoViableAltException nvae_d6s1 =
                        new NoViableAltException("", 6, 1, input);

                    throw nvae_d6s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d6s0 =
                    new NoViableAltException("", 6, 0, input);

                throw nvae_d6s0;
            }
            switch (alt6) 
            {
                case 1 :
                    // Movimentum.g:298:16: '.c'
                    {
                    	Match(".c"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:298:23: '.color'
                    {
                    	Match(".color"); 


                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COLOR"

    // $ANTLR start "LENGTH"
    public void mLENGTH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LENGTH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:299:14: ( '.l' | '.length' )
            int alt7 = 2;
            int LA7_0 = input.LA(1);

            if ( (LA7_0 == '.') )
            {
                int LA7_1 = input.LA(2);

                if ( (LA7_1 == 'l') )
                {
                    int LA7_2 = input.LA(3);

                    if ( (LA7_2 == 'e') )
                    {
                        alt7 = 2;
                    }
                    else 
                    {
                        alt7 = 1;}
                }
                else 
                {
                    NoViableAltException nvae_d7s1 =
                        new NoViableAltException("", 7, 1, input);

                    throw nvae_d7s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d7s0 =
                    new NoViableAltException("", 7, 0, input);

                throw nvae_d7s0;
            }
            switch (alt7) 
            {
                case 1 :
                    // Movimentum.g:299:16: '.l'
                    {
                    	Match(".l"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:299:23: '.length'
                    {
                    	Match(".length"); 


                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LENGTH"

    // $ANTLR start "NUMBER"
    public void mNUMBER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NUMBER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:302:7: ( ( '0' .. '9' )+ ( '.' ( '0' .. '9' )* )? ( ( 'E' | 'e' ) ( '-' )? ( '0' .. '9' )+ )? )
            // Movimentum.g:302:9: ( '0' .. '9' )+ ( '.' ( '0' .. '9' )* )? ( ( 'E' | 'e' ) ( '-' )? ( '0' .. '9' )+ )?
            {
            	// Movimentum.g:302:9: ( '0' .. '9' )+
            	int cnt8 = 0;
            	do 
            	{
            	    int alt8 = 2;
            	    int LA8_0 = input.LA(1);

            	    if ( ((LA8_0 >= '0' && LA8_0 <= '9')) )
            	    {
            	        alt8 = 1;
            	    }


            	    switch (alt8) 
            		{
            			case 1 :
            			    // Movimentum.g:302:10: '0' .. '9'
            			    {
            			    	MatchRange('0','9'); 

            			    }
            			    break;

            			default:
            			    if ( cnt8 >= 1 ) goto loop8;
            		            EarlyExitException eee =
            		                new EarlyExitException(8, input);
            		            throw eee;
            	    }
            	    cnt8++;
            	} while (true);

            	loop8:
            		;	// Stops C# compiler whinging that label 'loop8' has no statements

            	// Movimentum.g:303:10: ( '.' ( '0' .. '9' )* )?
            	int alt10 = 2;
            	int LA10_0 = input.LA(1);

            	if ( (LA10_0 == '.') )
            	{
            	    alt10 = 1;
            	}
            	switch (alt10) 
            	{
            	    case 1 :
            	        // Movimentum.g:303:12: '.' ( '0' .. '9' )*
            	        {
            	        	Match('.'); 
            	        	// Movimentum.g:304:12: ( '0' .. '9' )*
            	        	do 
            	        	{
            	        	    int alt9 = 2;
            	        	    int LA9_0 = input.LA(1);

            	        	    if ( ((LA9_0 >= '0' && LA9_0 <= '9')) )
            	        	    {
            	        	        alt9 = 1;
            	        	    }


            	        	    switch (alt9) 
            	        		{
            	        			case 1 :
            	        			    // Movimentum.g:304:13: '0' .. '9'
            	        			    {
            	        			    	MatchRange('0','9'); 

            	        			    }
            	        			    break;

            	        			default:
            	        			    goto loop9;
            	        	    }
            	        	} while (true);

            	        	loop9:
            	        		;	// Stops C# compiler whining that label 'loop9' has no statements


            	        }
            	        break;

            	}

            	// Movimentum.g:306:9: ( ( 'E' | 'e' ) ( '-' )? ( '0' .. '9' )+ )?
            	int alt13 = 2;
            	int LA13_0 = input.LA(1);

            	if ( (LA13_0 == 'E' || LA13_0 == 'e') )
            	{
            	    alt13 = 1;
            	}
            	switch (alt13) 
            	{
            	    case 1 :
            	        // Movimentum.g:306:11: ( 'E' | 'e' ) ( '-' )? ( '0' .. '9' )+
            	        {
            	        	if ( input.LA(1) == 'E' || input.LA(1) == 'e' ) 
            	        	{
            	        	    input.Consume();

            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    Recover(mse);
            	        	    throw mse;}

            	        	// Movimentum.g:307:11: ( '-' )?
            	        	int alt11 = 2;
            	        	int LA11_0 = input.LA(1);

            	        	if ( (LA11_0 == '-') )
            	        	{
            	        	    alt11 = 1;
            	        	}
            	        	switch (alt11) 
            	        	{
            	        	    case 1 :
            	        	        // Movimentum.g:307:12: '-'
            	        	        {
            	        	        	Match('-'); 

            	        	        }
            	        	        break;

            	        	}

            	        	// Movimentum.g:308:11: ( '0' .. '9' )+
            	        	int cnt12 = 0;
            	        	do 
            	        	{
            	        	    int alt12 = 2;
            	        	    int LA12_0 = input.LA(1);

            	        	    if ( ((LA12_0 >= '0' && LA12_0 <= '9')) )
            	        	    {
            	        	        alt12 = 1;
            	        	    }


            	        	    switch (alt12) 
            	        		{
            	        			case 1 :
            	        			    // Movimentum.g:308:12: '0' .. '9'
            	        			    {
            	        			    	MatchRange('0','9'); 

            	        			    }
            	        			    break;

            	        			default:
            	        			    if ( cnt12 >= 1 ) goto loop12;
            	        		            EarlyExitException eee =
            	        		                new EarlyExitException(12, input);
            	        		            throw eee;
            	        	    }
            	        	    cnt12++;
            	        	} while (true);

            	        	loop12:
            	        		;	// Stops C# compiler whinging that label 'loop12' has no statements


            	        }
            	        break;

            	}


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NUMBER"

    // $ANTLR start "WHITESPACE"
    public void mWHITESPACE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WHITESPACE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:313:7: ( ( '\\t' | ' ' | '\\r' | '\\n' | '\\u000C' )+ )
            // Movimentum.g:313:9: ( '\\t' | ' ' | '\\r' | '\\n' | '\\u000C' )+
            {
            	// Movimentum.g:313:9: ( '\\t' | ' ' | '\\r' | '\\n' | '\\u000C' )+
            	int cnt14 = 0;
            	do 
            	{
            	    int alt14 = 2;
            	    int LA14_0 = input.LA(1);

            	    if ( ((LA14_0 >= '\t' && LA14_0 <= '\n') || (LA14_0 >= '\f' && LA14_0 <= '\r') || LA14_0 == ' ') )
            	    {
            	        alt14 = 1;
            	    }


            	    switch (alt14) 
            		{
            			case 1 :
            			    // Movimentum.g:
            			    {
            			    	if ( (input.LA(1) >= '\t' && input.LA(1) <= '\n') || (input.LA(1) >= '\f' && input.LA(1) <= '\r') || input.LA(1) == ' ' ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    if ( cnt14 >= 1 ) goto loop14;
            		            EarlyExitException eee =
            		                new EarlyExitException(14, input);
            		            throw eee;
            	    }
            	    cnt14++;
            	} while (true);

            	loop14:
            		;	// Stops C# compiler whinging that label 'loop14' has no statements

            	 _channel = HIDDEN; 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WHITESPACE"

    // $ANTLR start "COMMENT"
    public void mCOMMENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMMENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:317:7: ( '/' '/' ( . )* ( '\\r' | '\\n' ) )
            // Movimentum.g:317:9: '/' '/' ( . )* ( '\\r' | '\\n' )
            {
            	Match('/'); 
            	Match('/'); 
            	// Movimentum.g:317:17: ( . )*
            	do 
            	{
            	    int alt15 = 2;
            	    int LA15_0 = input.LA(1);

            	    if ( (LA15_0 == '\n' || LA15_0 == '\r') )
            	    {
            	        alt15 = 2;
            	    }
            	    else if ( ((LA15_0 >= '\u0000' && LA15_0 <= '\t') || (LA15_0 >= '\u000B' && LA15_0 <= '\f') || (LA15_0 >= '\u000E' && LA15_0 <= '\uFFFF')) )
            	    {
            	        alt15 = 1;
            	    }


            	    switch (alt15) 
            		{
            			case 1 :
            			    // Movimentum.g:317:17: .
            			    {
            			    	MatchAny(); 

            			    }
            			    break;

            			default:
            			    goto loop15;
            	    }
            	} while (true);

            	loop15:
            		;	// Stops C# compiler whining that label 'loop15' has no statements

            	if ( input.LA(1) == '\n' || input.LA(1) == '\r' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	 _channel = HIDDEN; 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMMENT"

    // $ANTLR start "IDENT"
    public void mIDENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IDENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:321:7: ( ( 'a' .. 'z' | 'A' .. 'Z' ) ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '_' )* )
            // Movimentum.g:321:9: ( 'a' .. 'z' | 'A' .. 'Z' ) ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '_' )*
            {
            	if ( (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	// Movimentum.g:321:28: ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '_' )*
            	do 
            	{
            	    int alt16 = 2;
            	    int LA16_0 = input.LA(1);

            	    if ( ((LA16_0 >= '0' && LA16_0 <= '9') || (LA16_0 >= 'A' && LA16_0 <= 'Z') || LA16_0 == '_' || (LA16_0 >= 'a' && LA16_0 <= 'z')) )
            	    {
            	        alt16 = 1;
            	    }


            	    switch (alt16) 
            		{
            			case 1 :
            			    // Movimentum.g:
            			    {
            			    	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop16;
            	    }
            	} while (true);

            	loop16:
            		;	// Stops C# compiler whining that label 'loop16' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IDENT"

    // $ANTLR start "FILENAME"
    public void mFILENAME() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FILENAME;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:327:7: ( '\\'' (~ ( '\\'' ) )* '\\'' )
            // Movimentum.g:327:10: '\\'' (~ ( '\\'' ) )* '\\''
            {
            	Match('\''); 
            	// Movimentum.g:328:9: (~ ( '\\'' ) )*
            	do 
            	{
            	    int alt17 = 2;
            	    int LA17_0 = input.LA(1);

            	    if ( ((LA17_0 >= '\u0000' && LA17_0 <= '&') || (LA17_0 >= '(' && LA17_0 <= '\uFFFF')) )
            	    {
            	        alt17 = 1;
            	    }


            	    switch (alt17) 
            		{
            			case 1 :
            			    // Movimentum.g:328:10: ~ ( '\\'' )
            			    {
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '&') || (input.LA(1) >= '(' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop17;
            	    }
            	} while (true);

            	loop17:
            		;	// Stops C# compiler whining that label 'loop17' has no statements

            	Match('\''); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FILENAME"

    override public void mTokens() // throws RecognitionException 
    {
        // Movimentum.g:1:8: ( T__23 | T__24 | T__25 | T__26 | T__27 | T__28 | T__29 | T__30 | T__31 | T__32 | T__33 | T__34 | T__35 | T__36 | T__37 | T__38 | T__39 | T__40 | T__41 | T__42 | T__43 | T__44 | T__45 | CONFIG | BAR | X | Y | Z | T | IV | SQRT | ROTATE | INTEGRAL | DIFFERENTIAL | ANGLE | COLOR | LENGTH | NUMBER | WHITESPACE | COMMENT | IDENT | FILENAME )
        int alt18 = 42;
        alt18 = dfa18.Predict(input);
        switch (alt18) 
        {
            case 1 :
                // Movimentum.g:1:10: T__23
                {
                	mT__23(); 

                }
                break;
            case 2 :
                // Movimentum.g:1:16: T__24
                {
                	mT__24(); 

                }
                break;
            case 3 :
                // Movimentum.g:1:22: T__25
                {
                	mT__25(); 

                }
                break;
            case 4 :
                // Movimentum.g:1:28: T__26
                {
                	mT__26(); 

                }
                break;
            case 5 :
                // Movimentum.g:1:34: T__27
                {
                	mT__27(); 

                }
                break;
            case 6 :
                // Movimentum.g:1:40: T__28
                {
                	mT__28(); 

                }
                break;
            case 7 :
                // Movimentum.g:1:46: T__29
                {
                	mT__29(); 

                }
                break;
            case 8 :
                // Movimentum.g:1:52: T__30
                {
                	mT__30(); 

                }
                break;
            case 9 :
                // Movimentum.g:1:58: T__31
                {
                	mT__31(); 

                }
                break;
            case 10 :
                // Movimentum.g:1:64: T__32
                {
                	mT__32(); 

                }
                break;
            case 11 :
                // Movimentum.g:1:70: T__33
                {
                	mT__33(); 

                }
                break;
            case 12 :
                // Movimentum.g:1:76: T__34
                {
                	mT__34(); 

                }
                break;
            case 13 :
                // Movimentum.g:1:82: T__35
                {
                	mT__35(); 

                }
                break;
            case 14 :
                // Movimentum.g:1:88: T__36
                {
                	mT__36(); 

                }
                break;
            case 15 :
                // Movimentum.g:1:94: T__37
                {
                	mT__37(); 

                }
                break;
            case 16 :
                // Movimentum.g:1:100: T__38
                {
                	mT__38(); 

                }
                break;
            case 17 :
                // Movimentum.g:1:106: T__39
                {
                	mT__39(); 

                }
                break;
            case 18 :
                // Movimentum.g:1:112: T__40
                {
                	mT__40(); 

                }
                break;
            case 19 :
                // Movimentum.g:1:118: T__41
                {
                	mT__41(); 

                }
                break;
            case 20 :
                // Movimentum.g:1:124: T__42
                {
                	mT__42(); 

                }
                break;
            case 21 :
                // Movimentum.g:1:130: T__43
                {
                	mT__43(); 

                }
                break;
            case 22 :
                // Movimentum.g:1:136: T__44
                {
                	mT__44(); 

                }
                break;
            case 23 :
                // Movimentum.g:1:142: T__45
                {
                	mT__45(); 

                }
                break;
            case 24 :
                // Movimentum.g:1:148: CONFIG
                {
                	mCONFIG(); 

                }
                break;
            case 25 :
                // Movimentum.g:1:155: BAR
                {
                	mBAR(); 

                }
                break;
            case 26 :
                // Movimentum.g:1:159: X
                {
                	mX(); 

                }
                break;
            case 27 :
                // Movimentum.g:1:161: Y
                {
                	mY(); 

                }
                break;
            case 28 :
                // Movimentum.g:1:163: Z
                {
                	mZ(); 

                }
                break;
            case 29 :
                // Movimentum.g:1:165: T
                {
                	mT(); 

                }
                break;
            case 30 :
                // Movimentum.g:1:167: IV
                {
                	mIV(); 

                }
                break;
            case 31 :
                // Movimentum.g:1:170: SQRT
                {
                	mSQRT(); 

                }
                break;
            case 32 :
                // Movimentum.g:1:175: ROTATE
                {
                	mROTATE(); 

                }
                break;
            case 33 :
                // Movimentum.g:1:182: INTEGRAL
                {
                	mINTEGRAL(); 

                }
                break;
            case 34 :
                // Movimentum.g:1:191: DIFFERENTIAL
                {
                	mDIFFERENTIAL(); 

                }
                break;
            case 35 :
                // Movimentum.g:1:204: ANGLE
                {
                	mANGLE(); 

                }
                break;
            case 36 :
                // Movimentum.g:1:210: COLOR
                {
                	mCOLOR(); 

                }
                break;
            case 37 :
                // Movimentum.g:1:216: LENGTH
                {
                	mLENGTH(); 

                }
                break;
            case 38 :
                // Movimentum.g:1:223: NUMBER
                {
                	mNUMBER(); 

                }
                break;
            case 39 :
                // Movimentum.g:1:230: WHITESPACE
                {
                	mWHITESPACE(); 

                }
                break;
            case 40 :
                // Movimentum.g:1:241: COMMENT
                {
                	mCOMMENT(); 

                }
                break;
            case 41 :
                // Movimentum.g:1:249: IDENT
                {
                	mIDENT(); 

                }
                break;
            case 42 :
                // Movimentum.g:1:255: FILENAME
                {
                	mFILENAME(); 

                }
                break;

        }

    }


    protected DFA18 dfa18;
	private void InitializeCyclicDFAs()
	{
	    this.dfa18 = new DFA18(this);
	}

    const string DFA18_eotS =
        "\x0c\uffff\x01\x1a\x01\x1c\x02\uffff\x01\x29\x01\x2b\x0b\uffff"+
        "\x01\x2f\x05\uffff\x01\x31\x0f\uffff";
    const string DFA18_eofS =
        "\x33\uffff";
    const string DFA18_minS =
        "\x01\x09\x0b\uffff\x02\x3d\x02\uffff\x01\x61\x01\x2f\x01\x32\x0a"+
        "\uffff\x01\x6f\x05\uffff\x01\x76\x0a\uffff\x01\x6c\x04\uffff";
    const string DFA18_maxS =
        "\x01\x7d\x0b\uffff\x02\x3d\x02\uffff\x01\x7a\x01\x2f\x01\x33\x0a"+
        "\uffff\x01\x6f\x05\uffff\x01\x76\x0a\uffff\x01\x6e\x04\uffff";
    const string DFA18_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x01\x03\x01\x04\x01\x05\x01\x06\x01"+
        "\x07\x01\x08\x01\x09\x01\x0a\x01\x0b\x02\uffff\x01\x10\x01\x11\x03"+
        "\uffff\x01\x16\x01\x17\x01\x26\x01\x27\x01\x29\x01\x2a\x01\x0e\x01"+
        "\x0c\x01\x0f\x01\x0d\x01\uffff\x01\x19\x01\x1a\x01\x1b\x01\x1c\x01"+
        "\x1d\x01\uffff\x01\x1f\x01\x20\x01\x22\x01\x23\x01\x25\x01\x12\x01"+
        "\x28\x01\x13\x01\x14\x01\x15\x01\uffff\x01\x24\x01\x1e\x01\x21\x01"+
        "\x18";
    const string DFA18_specialS =
        "\x33\uffff}>";
    static readonly string[] DFA18_transitionS = {
            "\x02\x16\x01\uffff\x02\x16\x12\uffff\x01\x16\x06\uffff\x01"+
            "\x18\x01\x01\x01\x03\x01\x0e\x01\x07\x01\x02\x01\x08\x01\x10"+
            "\x01\x11\x0a\x15\x01\x05\x01\x04\x01\x0c\x01\x06\x01\x0d\x01"+
            "\uffff\x01\x0b\x1a\x17\x01\x09\x01\uffff\x01\x0a\x01\x12\x01"+
            "\x0f\x01\uffff\x1a\x17\x01\x13\x01\uffff\x01\x14",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x19",
            "\x01\x1b",
            "",
            "",
            "\x01\x27\x01\x1e\x01\x1d\x01\x26\x04\uffff\x01\x23\x02\uffff"+
            "\x01\x28\x04\uffff\x01\x24\x01\x25\x01\x24\x01\x22\x03\uffff"+
            "\x01\x1f\x01\x20\x01\x21",
            "\x01\x2a",
            "\x01\x2c\x01\x2d",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x2e",
            "",
            "",
            "",
            "",
            "",
            "\x01\x30",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x2f\x01\uffff\x01\x32",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA18_eot = DFA.UnpackEncodedString(DFA18_eotS);
    static readonly short[] DFA18_eof = DFA.UnpackEncodedString(DFA18_eofS);
    static readonly char[] DFA18_min = DFA.UnpackEncodedStringToUnsignedChars(DFA18_minS);
    static readonly char[] DFA18_max = DFA.UnpackEncodedStringToUnsignedChars(DFA18_maxS);
    static readonly short[] DFA18_accept = DFA.UnpackEncodedString(DFA18_acceptS);
    static readonly short[] DFA18_special = DFA.UnpackEncodedString(DFA18_specialS);
    static readonly short[][] DFA18_transition = DFA.UnpackEncodedStringArray(DFA18_transitionS);

    protected class DFA18 : DFA
    {
        public DFA18(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 18;
            this.eot = DFA18_eot;
            this.eof = DFA18_eof;
            this.min = DFA18_min;
            this.max = DFA18_max;
            this.accept = DFA18_accept;
            this.special = DFA18_special;
            this.transition = DFA18_transition;

        }

        override public string Description
        {
            get { return "1:1: Tokens : ( T__23 | T__24 | T__25 | T__26 | T__27 | T__28 | T__29 | T__30 | T__31 | T__32 | T__33 | T__34 | T__35 | T__36 | T__37 | T__38 | T__39 | T__40 | T__41 | T__42 | T__43 | T__44 | T__45 | CONFIG | BAR | X | Y | Z | T | IV | SQRT | ROTATE | INTEGRAL | DIFFERENTIAL | ANGLE | COLOR | LENGTH | NUMBER | WHITESPACE | COMMENT | IDENT | FILENAME );"; }
        }

    }

 
    
}
}