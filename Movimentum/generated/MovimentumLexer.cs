// $ANTLR 3.1.1 Movimentum.g 2012-04-23 12:51:23
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
    public const int ANGLE = 13;
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
    public const int X = 7;
    public const int LENGTH = 16;
    public const int Z = 9;
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
    public const int FILENAME = 6;
    public const int T__39 = 39;
    public const int IV = 15;
    public const int IDENT = 5;
    public const int COMMENT = 20;
    public const int DIFFERENTIAL = 12;

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

    // $ANTLR start "T__21"
    public void mT__21() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__21;
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
    // $ANTLR end "T__21"

    // $ANTLR start "T__22"
    public void mT__22() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__22;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:10:7: ( ')' )
            // Movimentum.g:10:9: ')'
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
    // $ANTLR end "T__22"

    // $ANTLR start "T__23"
    public void mT__23() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__23;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:11:7: ( ';' )
            // Movimentum.g:11:9: ';'
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
    // $ANTLR end "T__23"

    // $ANTLR start "T__24"
    public void mT__24() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__24;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:12:7: ( ':' )
            // Movimentum.g:12:9: ':'
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
    // $ANTLR end "T__24"

    // $ANTLR start "T__25"
    public void mT__25() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__25;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:13:7: ( '=' )
            // Movimentum.g:13:9: '='
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
    // $ANTLR end "T__25"

    // $ANTLR start "T__26"
    public void mT__26() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__26;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:14:7: ( '+' )
            // Movimentum.g:14:9: '+'
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
    // $ANTLR end "T__26"

    // $ANTLR start "T__27"
    public void mT__27() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__27;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:15:7: ( '-' )
            // Movimentum.g:15:9: '-'
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
    // $ANTLR end "T__27"

    // $ANTLR start "T__28"
    public void mT__28() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__28;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:16:7: ( '[' )
            // Movimentum.g:16:9: '['
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
    // $ANTLR end "T__28"

    // $ANTLR start "T__29"
    public void mT__29() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__29;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:17:7: ( ',' )
            // Movimentum.g:17:9: ','
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
    // $ANTLR end "T__29"

    // $ANTLR start "T__30"
    public void mT__30() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__30;
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
    // $ANTLR end "T__30"

    // $ANTLR start "T__31"
    public void mT__31() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__31;
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
    // $ANTLR end "T__31"

    // $ANTLR start "T__32"
    public void mT__32() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__32;
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
    // $ANTLR end "T__32"

    // $ANTLR start "T__33"
    public void mT__33() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__33;
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
    // $ANTLR end "T__33"

    // $ANTLR start "T__34"
    public void mT__34() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__34;
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
    // $ANTLR end "T__34"

    // $ANTLR start "T__35"
    public void mT__35() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__35;
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
    // $ANTLR end "T__35"

    // $ANTLR start "T__36"
    public void mT__36() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__36;
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
    // $ANTLR end "T__36"

    // $ANTLR start "T__37"
    public void mT__37() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__37;
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
    // $ANTLR end "T__37"

    // $ANTLR start "T__38"
    public void mT__38() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__38;
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
    // $ANTLR end "T__38"

    // $ANTLR start "T__39"
    public void mT__39() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = T__39;
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
    // $ANTLR end "T__39"

    // $ANTLR start "CONFIG"
    public void mCONFIG() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CONFIG;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:247:8: ( '.config' )
            // Movimentum.g:247:10: '.config'
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

    // $ANTLR start "X"
    public void mX() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = X;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:249:3: ( '.x' )
            // Movimentum.g:249:6: '.x'
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
            // Movimentum.g:250:3: ( '.y' )
            // Movimentum.g:250:6: '.y'
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
            // Movimentum.g:251:3: ( '.z' )
            // Movimentum.g:251:6: '.z'
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
            // Movimentum.g:252:3: ( '.t' )
            // Movimentum.g:252:6: '.t'
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
            // Movimentum.g:253:4: ( '.iv' )
            // Movimentum.g:253:7: '.iv'
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

    // $ANTLR start "ROTATE"
    public void mROTATE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ROTATE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // Movimentum.g:255:14: ( '.r' | '.rotate' )
            int alt1 = 2;
            int LA1_0 = input.LA(1);

            if ( (LA1_0 == '.') )
            {
                int LA1_1 = input.LA(2);

                if ( (LA1_1 == 'r') )
                {
                    int LA1_2 = input.LA(3);

                    if ( (LA1_2 == 'o') )
                    {
                        alt1 = 2;
                    }
                    else 
                    {
                        alt1 = 1;}
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
                    // Movimentum.g:255:16: '.r'
                    {
                    	Match(".r"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:255:23: '.rotate'
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
            // Movimentum.g:256:14: ( '.i' | '.integral' )
            int alt2 = 2;
            int LA2_0 = input.LA(1);

            if ( (LA2_0 == '.') )
            {
                int LA2_1 = input.LA(2);

                if ( (LA2_1 == 'i') )
                {
                    int LA2_2 = input.LA(3);

                    if ( (LA2_2 == 'n') )
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
                    // Movimentum.g:256:16: '.i'
                    {
                    	Match(".i"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:256:23: '.integral'
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
            // Movimentum.g:257:14: ( '.d' | '.differential' )
            int alt3 = 2;
            int LA3_0 = input.LA(1);

            if ( (LA3_0 == '.') )
            {
                int LA3_1 = input.LA(2);

                if ( (LA3_1 == 'd') )
                {
                    int LA3_2 = input.LA(3);

                    if ( (LA3_2 == 'i') )
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
                    // Movimentum.g:257:16: '.d'
                    {
                    	Match(".d"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:257:23: '.differential'
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
            // Movimentum.g:258:14: ( '.a' | '.angle' )
            int alt4 = 2;
            int LA4_0 = input.LA(1);

            if ( (LA4_0 == '.') )
            {
                int LA4_1 = input.LA(2);

                if ( (LA4_1 == 'a') )
                {
                    int LA4_2 = input.LA(3);

                    if ( (LA4_2 == 'n') )
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
                    // Movimentum.g:258:16: '.a'
                    {
                    	Match(".a"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:258:23: '.angle'
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
            // Movimentum.g:259:14: ( '.c' | '.color' )
            int alt5 = 2;
            int LA5_0 = input.LA(1);

            if ( (LA5_0 == '.') )
            {
                int LA5_1 = input.LA(2);

                if ( (LA5_1 == 'c') )
                {
                    int LA5_2 = input.LA(3);

                    if ( (LA5_2 == 'o') )
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
                    // Movimentum.g:259:16: '.c'
                    {
                    	Match(".c"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:259:23: '.color'
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
            // Movimentum.g:260:14: ( '.l' | '.LENGTH' )
            int alt6 = 2;
            int LA6_0 = input.LA(1);

            if ( (LA6_0 == '.') )
            {
                int LA6_1 = input.LA(2);

                if ( (LA6_1 == 'l') )
                {
                    alt6 = 1;
                }
                else if ( (LA6_1 == 'L') )
                {
                    alt6 = 2;
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
                    // Movimentum.g:260:16: '.l'
                    {
                    	Match(".l"); 


                    }
                    break;
                case 2 :
                    // Movimentum.g:260:23: '.LENGTH'
                    {
                    	Match(".LENGTH"); 


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
            // Movimentum.g:263:7: ( ( '0' .. '9' )+ ( '.' ( '0' .. '9' )* )? ( ( 'E' | 'e' ) ( '-' )? ( '0' .. '9' )+ )? )
            // Movimentum.g:263:9: ( '0' .. '9' )+ ( '.' ( '0' .. '9' )* )? ( ( 'E' | 'e' ) ( '-' )? ( '0' .. '9' )+ )?
            {
            	// Movimentum.g:263:9: ( '0' .. '9' )+
            	int cnt7 = 0;
            	do 
            	{
            	    int alt7 = 2;
            	    int LA7_0 = input.LA(1);

            	    if ( ((LA7_0 >= '0' && LA7_0 <= '9')) )
            	    {
            	        alt7 = 1;
            	    }


            	    switch (alt7) 
            		{
            			case 1 :
            			    // Movimentum.g:263:10: '0' .. '9'
            			    {
            			    	MatchRange('0','9'); 

            			    }
            			    break;

            			default:
            			    if ( cnt7 >= 1 ) goto loop7;
            		            EarlyExitException eee =
            		                new EarlyExitException(7, input);
            		            throw eee;
            	    }
            	    cnt7++;
            	} while (true);

            	loop7:
            		;	// Stops C# compiler whinging that label 'loop7' has no statements

            	// Movimentum.g:264:10: ( '.' ( '0' .. '9' )* )?
            	int alt9 = 2;
            	int LA9_0 = input.LA(1);

            	if ( (LA9_0 == '.') )
            	{
            	    alt9 = 1;
            	}
            	switch (alt9) 
            	{
            	    case 1 :
            	        // Movimentum.g:264:12: '.' ( '0' .. '9' )*
            	        {
            	        	Match('.'); 
            	        	// Movimentum.g:265:12: ( '0' .. '9' )*
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
            	        			    // Movimentum.g:265:13: '0' .. '9'
            	        			    {
            	        			    	MatchRange('0','9'); 

            	        			    }
            	        			    break;

            	        			default:
            	        			    goto loop8;
            	        	    }
            	        	} while (true);

            	        	loop8:
            	        		;	// Stops C# compiler whining that label 'loop8' has no statements


            	        }
            	        break;

            	}

            	// Movimentum.g:267:9: ( ( 'E' | 'e' ) ( '-' )? ( '0' .. '9' )+ )?
            	int alt12 = 2;
            	int LA12_0 = input.LA(1);

            	if ( (LA12_0 == 'E' || LA12_0 == 'e') )
            	{
            	    alt12 = 1;
            	}
            	switch (alt12) 
            	{
            	    case 1 :
            	        // Movimentum.g:267:11: ( 'E' | 'e' ) ( '-' )? ( '0' .. '9' )+
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

            	        	// Movimentum.g:268:11: ( '-' )?
            	        	int alt10 = 2;
            	        	int LA10_0 = input.LA(1);

            	        	if ( (LA10_0 == '-') )
            	        	{
            	        	    alt10 = 1;
            	        	}
            	        	switch (alt10) 
            	        	{
            	        	    case 1 :
            	        	        // Movimentum.g:268:12: '-'
            	        	        {
            	        	        	Match('-'); 

            	        	        }
            	        	        break;

            	        	}

            	        	// Movimentum.g:269:11: ( '0' .. '9' )+
            	        	int cnt11 = 0;
            	        	do 
            	        	{
            	        	    int alt11 = 2;
            	        	    int LA11_0 = input.LA(1);

            	        	    if ( ((LA11_0 >= '0' && LA11_0 <= '9')) )
            	        	    {
            	        	        alt11 = 1;
            	        	    }


            	        	    switch (alt11) 
            	        		{
            	        			case 1 :
            	        			    // Movimentum.g:269:12: '0' .. '9'
            	        			    {
            	        			    	MatchRange('0','9'); 

            	        			    }
            	        			    break;

            	        			default:
            	        			    if ( cnt11 >= 1 ) goto loop11;
            	        		            EarlyExitException eee =
            	        		                new EarlyExitException(11, input);
            	        		            throw eee;
            	        	    }
            	        	    cnt11++;
            	        	} while (true);

            	        	loop11:
            	        		;	// Stops C# compiler whinging that label 'loop11' has no statements


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
            // Movimentum.g:274:7: ( ( '\\t' | ' ' | '\\r' | '\\n' | '\\u000C' )+ )
            // Movimentum.g:274:9: ( '\\t' | ' ' | '\\r' | '\\n' | '\\u000C' )+
            {
            	// Movimentum.g:274:9: ( '\\t' | ' ' | '\\r' | '\\n' | '\\u000C' )+
            	int cnt13 = 0;
            	do 
            	{
            	    int alt13 = 2;
            	    int LA13_0 = input.LA(1);

            	    if ( ((LA13_0 >= '\t' && LA13_0 <= '\n') || (LA13_0 >= '\f' && LA13_0 <= '\r') || LA13_0 == ' ') )
            	    {
            	        alt13 = 1;
            	    }


            	    switch (alt13) 
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
            			    if ( cnt13 >= 1 ) goto loop13;
            		            EarlyExitException eee =
            		                new EarlyExitException(13, input);
            		            throw eee;
            	    }
            	    cnt13++;
            	} while (true);

            	loop13:
            		;	// Stops C# compiler whinging that label 'loop13' has no statements

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
            // Movimentum.g:278:7: ( '/' '/' ( . )* ( '\\r' | '\\n' ) )
            // Movimentum.g:278:9: '/' '/' ( . )* ( '\\r' | '\\n' )
            {
            	Match('/'); 
            	Match('/'); 
            	// Movimentum.g:278:17: ( . )*
            	do 
            	{
            	    int alt14 = 2;
            	    int LA14_0 = input.LA(1);

            	    if ( (LA14_0 == '\n' || LA14_0 == '\r') )
            	    {
            	        alt14 = 2;
            	    }
            	    else if ( ((LA14_0 >= '\u0000' && LA14_0 <= '\t') || (LA14_0 >= '\u000B' && LA14_0 <= '\f') || (LA14_0 >= '\u000E' && LA14_0 <= '\uFFFF')) )
            	    {
            	        alt14 = 1;
            	    }


            	    switch (alt14) 
            		{
            			case 1 :
            			    // Movimentum.g:278:17: .
            			    {
            			    	MatchAny(); 

            			    }
            			    break;

            			default:
            			    goto loop14;
            	    }
            	} while (true);

            	loop14:
            		;	// Stops C# compiler whining that label 'loop14' has no statements

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
            // Movimentum.g:282:7: ( ( 'a' .. 'z' | 'A' .. 'Z' ) ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '_' )* )
            // Movimentum.g:282:9: ( 'a' .. 'z' | 'A' .. 'Z' ) ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '_' )*
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

            	// Movimentum.g:282:28: ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '_' )*
            	do 
            	{
            	    int alt15 = 2;
            	    int LA15_0 = input.LA(1);

            	    if ( ((LA15_0 >= '0' && LA15_0 <= '9') || (LA15_0 >= 'A' && LA15_0 <= 'Z') || LA15_0 == '_' || (LA15_0 >= 'a' && LA15_0 <= 'z')) )
            	    {
            	        alt15 = 1;
            	    }


            	    switch (alt15) 
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
            			    goto loop15;
            	    }
            	} while (true);

            	loop15:
            		;	// Stops C# compiler whining that label 'loop15' has no statements


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
            // Movimentum.g:288:7: ( '\\'' (~ ( '\\'' ) )* '\\'' )
            // Movimentum.g:288:10: '\\'' (~ ( '\\'' ) )* '\\''
            {
            	Match('\''); 
            	// Movimentum.g:289:9: (~ ( '\\'' ) )*
            	do 
            	{
            	    int alt16 = 2;
            	    int LA16_0 = input.LA(1);

            	    if ( ((LA16_0 >= '\u0000' && LA16_0 <= '&') || (LA16_0 >= '(' && LA16_0 <= '\uFFFF')) )
            	    {
            	        alt16 = 1;
            	    }


            	    switch (alt16) 
            		{
            			case 1 :
            			    // Movimentum.g:289:10: ~ ( '\\'' )
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
            			    goto loop16;
            	    }
            	} while (true);

            	loop16:
            		;	// Stops C# compiler whining that label 'loop16' has no statements

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
        // Movimentum.g:1:8: ( T__21 | T__22 | T__23 | T__24 | T__25 | T__26 | T__27 | T__28 | T__29 | T__30 | T__31 | T__32 | T__33 | T__34 | T__35 | T__36 | T__37 | T__38 | T__39 | CONFIG | X | Y | Z | T | IV | ROTATE | INTEGRAL | DIFFERENTIAL | ANGLE | COLOR | LENGTH | NUMBER | WHITESPACE | COMMENT | IDENT | FILENAME )
        int alt17 = 36;
        alt17 = dfa17.Predict(input);
        switch (alt17) 
        {
            case 1 :
                // Movimentum.g:1:10: T__21
                {
                	mT__21(); 

                }
                break;
            case 2 :
                // Movimentum.g:1:16: T__22
                {
                	mT__22(); 

                }
                break;
            case 3 :
                // Movimentum.g:1:22: T__23
                {
                	mT__23(); 

                }
                break;
            case 4 :
                // Movimentum.g:1:28: T__24
                {
                	mT__24(); 

                }
                break;
            case 5 :
                // Movimentum.g:1:34: T__25
                {
                	mT__25(); 

                }
                break;
            case 6 :
                // Movimentum.g:1:40: T__26
                {
                	mT__26(); 

                }
                break;
            case 7 :
                // Movimentum.g:1:46: T__27
                {
                	mT__27(); 

                }
                break;
            case 8 :
                // Movimentum.g:1:52: T__28
                {
                	mT__28(); 

                }
                break;
            case 9 :
                // Movimentum.g:1:58: T__29
                {
                	mT__29(); 

                }
                break;
            case 10 :
                // Movimentum.g:1:64: T__30
                {
                	mT__30(); 

                }
                break;
            case 11 :
                // Movimentum.g:1:70: T__31
                {
                	mT__31(); 

                }
                break;
            case 12 :
                // Movimentum.g:1:76: T__32
                {
                	mT__32(); 

                }
                break;
            case 13 :
                // Movimentum.g:1:82: T__33
                {
                	mT__33(); 

                }
                break;
            case 14 :
                // Movimentum.g:1:88: T__34
                {
                	mT__34(); 

                }
                break;
            case 15 :
                // Movimentum.g:1:94: T__35
                {
                	mT__35(); 

                }
                break;
            case 16 :
                // Movimentum.g:1:100: T__36
                {
                	mT__36(); 

                }
                break;
            case 17 :
                // Movimentum.g:1:106: T__37
                {
                	mT__37(); 

                }
                break;
            case 18 :
                // Movimentum.g:1:112: T__38
                {
                	mT__38(); 

                }
                break;
            case 19 :
                // Movimentum.g:1:118: T__39
                {
                	mT__39(); 

                }
                break;
            case 20 :
                // Movimentum.g:1:124: CONFIG
                {
                	mCONFIG(); 

                }
                break;
            case 21 :
                // Movimentum.g:1:131: X
                {
                	mX(); 

                }
                break;
            case 22 :
                // Movimentum.g:1:133: Y
                {
                	mY(); 

                }
                break;
            case 23 :
                // Movimentum.g:1:135: Z
                {
                	mZ(); 

                }
                break;
            case 24 :
                // Movimentum.g:1:137: T
                {
                	mT(); 

                }
                break;
            case 25 :
                // Movimentum.g:1:139: IV
                {
                	mIV(); 

                }
                break;
            case 26 :
                // Movimentum.g:1:142: ROTATE
                {
                	mROTATE(); 

                }
                break;
            case 27 :
                // Movimentum.g:1:149: INTEGRAL
                {
                	mINTEGRAL(); 

                }
                break;
            case 28 :
                // Movimentum.g:1:158: DIFFERENTIAL
                {
                	mDIFFERENTIAL(); 

                }
                break;
            case 29 :
                // Movimentum.g:1:171: ANGLE
                {
                	mANGLE(); 

                }
                break;
            case 30 :
                // Movimentum.g:1:177: COLOR
                {
                	mCOLOR(); 

                }
                break;
            case 31 :
                // Movimentum.g:1:183: LENGTH
                {
                	mLENGTH(); 

                }
                break;
            case 32 :
                // Movimentum.g:1:190: NUMBER
                {
                	mNUMBER(); 

                }
                break;
            case 33 :
                // Movimentum.g:1:197: WHITESPACE
                {
                	mWHITESPACE(); 

                }
                break;
            case 34 :
                // Movimentum.g:1:208: COMMENT
                {
                	mCOMMENT(); 

                }
                break;
            case 35 :
                // Movimentum.g:1:216: IDENT
                {
                	mIDENT(); 

                }
                break;
            case 36 :
                // Movimentum.g:1:222: FILENAME
                {
                	mFILENAME(); 

                }
                break;

        }

    }


    protected DFA17 dfa17;
	private void InitializeCyclicDFAs()
	{
	    this.dfa17 = new DFA17(this);
	}

    const string DFA17_eotS =
        "\x0c\uffff\x01\x17\x01\x19\x02\uffff\x01\x24\x01\x26\x08\uffff"+
        "\x01\x28\x04\uffff\x01\x2a\x0c\uffff";
    const string DFA17_eofS =
        "\x2c\uffff";
    const string DFA17_minS =
        "\x01\x09\x0b\uffff\x02\x3d\x02\uffff\x01\x4c\x01\x2f\x08\uffff"+
        "\x01\x6f\x04\uffff\x01\x76\x07\uffff\x01\x6c\x04\uffff";
    const string DFA17_maxS =
        "\x01\x7a\x0b\uffff\x02\x3d\x02\uffff\x01\x7a\x01\x2f\x08\uffff"+
        "\x01\x6f\x04\uffff\x01\x76\x07\uffff\x01\x6e\x04\uffff";
    const string DFA17_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x01\x03\x01\x04\x01\x05\x01\x06\x01"+
        "\x07\x01\x08\x01\x09\x01\x0a\x01\x0b\x02\uffff\x01\x10\x01\x11\x02"+
        "\uffff\x01\x20\x01\x21\x01\x23\x01\x24\x01\x0e\x01\x0c\x01\x0f\x01"+
        "\x0d\x01\uffff\x01\x15\x01\x16\x01\x17\x01\x18\x01\uffff\x01\x1a"+
        "\x01\x1c\x01\x1d\x01\x1f\x01\x12\x01\x22\x01\x13\x01\uffff\x01\x1e"+
        "\x01\x19\x01\x1b\x01\x14";
    const string DFA17_specialS =
        "\x2c\uffff}>";
    static readonly string[] DFA17_transitionS = {
            "\x02\x13\x01\uffff\x02\x13\x12\uffff\x01\x13\x06\uffff\x01"+
            "\x15\x01\x01\x01\x02\x01\x0e\x01\x06\x01\x09\x01\x07\x01\x10"+
            "\x01\x11\x0a\x12\x01\x04\x01\x03\x01\x0c\x01\x05\x01\x0d\x01"+
            "\uffff\x01\x0b\x1a\x14\x01\x08\x01\uffff\x01\x0a\x01\uffff\x01"+
            "\x0f\x01\uffff\x1a\x14",
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
            "\x01\x16",
            "\x01\x18",
            "",
            "",
            "\x01\x23\x14\uffff\x01\x22\x01\uffff\x01\x1a\x01\x21\x04\uffff"+
            "\x01\x1f\x02\uffff\x01\x23\x05\uffff\x01\x20\x01\uffff\x01\x1e"+
            "\x03\uffff\x01\x1b\x01\x1c\x01\x1d",
            "\x01\x25",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x27",
            "",
            "",
            "",
            "",
            "\x01\x29",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x28\x01\uffff\x01\x2b",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA17_eot = DFA.UnpackEncodedString(DFA17_eotS);
    static readonly short[] DFA17_eof = DFA.UnpackEncodedString(DFA17_eofS);
    static readonly char[] DFA17_min = DFA.UnpackEncodedStringToUnsignedChars(DFA17_minS);
    static readonly char[] DFA17_max = DFA.UnpackEncodedStringToUnsignedChars(DFA17_maxS);
    static readonly short[] DFA17_accept = DFA.UnpackEncodedString(DFA17_acceptS);
    static readonly short[] DFA17_special = DFA.UnpackEncodedString(DFA17_specialS);
    static readonly short[][] DFA17_transition = DFA.UnpackEncodedStringArray(DFA17_transitionS);

    protected class DFA17 : DFA
    {
        public DFA17(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 17;
            this.eot = DFA17_eot;
            this.eof = DFA17_eof;
            this.min = DFA17_min;
            this.max = DFA17_max;
            this.accept = DFA17_accept;
            this.special = DFA17_special;
            this.transition = DFA17_transition;

        }

        override public string Description
        {
            get { return "1:1: Tokens : ( T__21 | T__22 | T__23 | T__24 | T__25 | T__26 | T__27 | T__28 | T__29 | T__30 | T__31 | T__32 | T__33 | T__34 | T__35 | T__36 | T__37 | T__38 | T__39 | CONFIG | X | Y | Z | T | IV | ROTATE | INTEGRAL | DIFFERENTIAL | ANGLE | COLOR | LENGTH | NUMBER | WHITESPACE | COMMENT | IDENT | FILENAME );"; }
        }

    }

 
    
}
}