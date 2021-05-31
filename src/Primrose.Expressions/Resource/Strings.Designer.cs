﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Primrose.Expressions.Resource {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Primrose.Expressions.Resource.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;bool&gt;.
        /// </summary>
        internal static string Bool {
            get {
                return ResourceManager.GetString("Bool", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to execute script function &apos;{0}&apos; at line {1}:{2} 
        ///Reason: {3}.
        /// </summary>
        internal static string Error_EvalException_4 {
            get {
                return ResourceManager.GetString("Error_EvalException_4", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to perform &apos;{0}&apos; operation on {1} in function &apos;{2}&apos; at line {3}:{4} 
        ///Reason: {5}.
        /// </summary>
        internal static string Error_EvalException_6 {
            get {
                return ResourceManager.GetString("Error_EvalException_6", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to perform &apos;{0}&apos; operation between {1} and {2} in function &apos;{3}&apos; at line {4}:{5} 
        ///Reason: {6}.
        /// </summary>
        internal static string Error_EvalException_7 {
            get {
                return ResourceManager.GetString("Error_EvalException_7", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Argument type mismatch for argument {0} of function &apos;{1}&apos;: expected {2}, received {3}.
        /// </summary>
        internal static string Error_EvalException_ArgumentTypeMismatch {
            get {
                return ResourceManager.GetString("Error_EvalException_ArgumentTypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The function &apos;{0}&apos; does not exist!.
        /// </summary>
        internal static string Error_EvalException_FunctionNotFound {
            get {
                return ResourceManager.GetString("Error_EvalException_FunctionNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to get undeclared variable &apos;{0}&apos;.
        /// </summary>
        internal static string Error_EvalException_Get_VariableNotFound {
            get {
                return ResourceManager.GetString("Error_EvalException_Get_VariableNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Incompatible types detected in array: {0}, {1}.
        /// </summary>
        internal static string Error_EvalException_IncompatibleArrayElement {
            get {
                return ResourceManager.GetString("Error_EvalException_IncompatibleArrayElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Incorrect number/type of parameters supplied to function &apos;{0}&apos;!.
        /// </summary>
        internal static string Error_EvalException_IncorrectParameters {
            get {
                return ResourceManager.GetString("Error_EvalException_IncorrectParameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to index a non-array: {0}.
        /// </summary>
        internal static string Error_EvalException_IndexOnNonArray {
            get {
                return ResourceManager.GetString("Error_EvalException_IndexOnNonArray", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Index ({0}) for an array (length: {1}) is out of range!.
        /// </summary>
        internal static string Error_EvalException_IndexOutOfRange {
            get {
                return ResourceManager.GetString("Error_EvalException_IndexOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to index an array with a non-integer!.
        /// </summary>
        internal static string Error_EvalException_InvalidArrayIndex {
            get {
                return ResourceManager.GetString("Error_EvalException_InvalidArrayIndex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Illegal assignment to &apos;{0} {1}&apos;: {2}.
        /// </summary>
        internal static string Error_EvalException_InvalidVariableAssignment {
            get {
                return ResourceManager.GetString("Error_EvalException_InvalidVariableAssignment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non-boolean value {0} found at start of conditional expression.
        /// </summary>
        internal static string Error_EvalException_NonBooleanConditional {
            get {
                return ResourceManager.GetString("Error_EvalException_NonBooleanConditional", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to set undeclared variable &apos;{0}&apos;.
        /// </summary>
        internal static string Error_EvalException_Set_VariableNotFound {
            get {
                return ResourceManager.GetString("Error_EvalException_Set_VariableNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unsupported array member type: {0}.
        /// </summary>
        internal static string Error_EvalException_UnsupportedArrayElement {
            get {
                return ResourceManager.GetString("Error_EvalException_UnsupportedArrayElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No candidate for next expression found!.
        /// </summary>
        internal static string Error_ExpressionNotFound {
            get {
                return ResourceManager.GetString("Error_ExpressionNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operation &apos;{0}&apos; incompatible between {1} and {2}.
        /// </summary>
        internal static string Error_IncompatibleBOp {
            get {
                return ResourceManager.GetString("Error_IncompatibleBOp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operation &apos;{0}&apos; incompatible with an indexed operation.
        /// </summary>
        internal static string Error_IncompatibleIndexOp {
            get {
                return ResourceManager.GetString("Error_IncompatibleIndexOp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operation &apos;{0}&apos; incompatible with {1}.
        /// </summary>
        internal static string Error_IncompatibleUOp {
            get {
                return ResourceManager.GetString("Error_IncompatibleUOp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted to read {0} value from a {1}.
        /// </summary>
        internal static string Error_InvalidValCastException {
            get {
                return ResourceManager.GetString("Error_InvalidValCastException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to match against any tokens at line {0} position {1} &apos;{2}&apos;.
        /// </summary>
        internal static string Error_Lexer_InvalidToken {
            get {
                return ResourceManager.GetString("Error_Lexer_InvalidToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected token &apos;{0}&apos; found in function &apos;{1}&apos; line {2}:{3}.
        ///Line: {4}.
        /// </summary>
        internal static string Error_ParseException_5 {
            get {
                return ResourceManager.GetString("Error_ParseException_5", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}
        ///Function &apos;{1}&apos; at line {2}:{3}. 
        ///Line: {4}.
        /// </summary>
        internal static string Error_ParseException_5M {
            get {
                return ResourceManager.GetString("Error_ParseException_5M", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected token &apos;{0}&apos; found in function &apos;{1}&apos; at line {2}:{3}. Expected: {4}.
        ///Line: {5}.
        /// </summary>
        internal static string Error_ParseException_6 {
            get {
                return ResourceManager.GetString("Error_ParseException_6", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Duplicate declaration of variable &apos;{0}&apos; in the same scope.
        /// </summary>
        internal static string Error_ParseException_DuplicateVariable {
            get {
                return ResourceManager.GetString("Error_ParseException_DuplicateVariable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Script file &apos;{0}&apos; is not found!.
        /// </summary>
        internal static string Error_ScriptFileNotFound {
            get {
                return ResourceManager.GetString("Error_ScriptFileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No candidate for next statement found!.
        /// </summary>
        internal static string Error_StatementNotFound {
            get {
                return ResourceManager.GetString("Error_StatementNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted assignment of an array of length {0} to {1}.
        /// </summary>
        internal static string Error_ValTypeMismatchException_Length {
            get {
                return ResourceManager.GetString("Error_ValTypeMismatchException_Length", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempted assignment of value of type &apos;{0}&apos; to {1}.
        /// </summary>
        internal static string Error_ValTypeMismatchException_Type {
            get {
                return ResourceManager.GetString("Error_ValTypeMismatchException_Type", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;float&gt;.
        /// </summary>
        internal static string Float {
            get {
                return ResourceManager.GetString("Float", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;func&gt;.
        /// </summary>
        internal static string Function {
            get {
                return ResourceManager.GetString("Function", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;hex_int&gt;.
        /// </summary>
        internal static string HexInt {
            get {
                return ResourceManager.GetString("HexInt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;int&gt;.
        /// </summary>
        internal static string Int {
            get {
                return ResourceManager.GetString("Int", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;null&gt;.
        /// </summary>
        internal static string Null {
            get {
                return ResourceManager.GetString("Null", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;string&gt;.
        /// </summary>
        internal static string String {
            get {
                return ResourceManager.GetString("String", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;???&gt;.
        /// </summary>
        internal static string Unknown {
            get {
                return ResourceManager.GetString("Unknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;variable&gt;.
        /// </summary>
        internal static string Variable {
            get {
                return ResourceManager.GetString("Variable", resourceCulture);
            }
        }
    }
}
