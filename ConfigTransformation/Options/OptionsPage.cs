﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Marazt.ConfigTransformation.Logging;
using Marazt.ConfigTransformation.Transformation;
using Microsoft.VisualStudio.Shell;

namespace Marazt.ConfigTransformation.Options
{
    /// <summary>
    /// Configuration page
    /// </summary>
    ///http://social.msdn.microsoft.com/Forums/vstudio/en-US/f15b121b-6874-4012-873a-3fc1a3fb9770/specified-cast-is-not-vaild?forum=vsx
    [CLSCompliant(false), ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Guid("1D9ECCF3-5D2F-4112-9B25-264596873DC9")]
    public class OptionsPage : DialogPage
    {
        #region Constants

        /// <summary>
        /// The transfomation file name regexp property name
        /// </summary>
        public const string TransfomationFileNameRegexpPropertyName = "TransfomationFileNameRegexp";

        /// <summary>
        /// The source file regexp match index property name
        /// </summary>
        public const string SourceFileRegexpMatchIndexPropertyName = "SourceFileRegexpMatchIndex";

        /// <summary>
        /// The nest transformation files property name
        /// </summary>
        public const string NestTransformationFilesPropertyName = "NestTransformationFiles";


        /// <summary>
        /// The write attributes on a separate line property name
        /// </summary>
        public const string WriteAttributesOnASeparateLinePropertyName = "WriteAttributesOnASeparateLine";

        #endregion Constants

        #region Fields

        /// <summary>
        /// The transfomation file name regexp
        /// </summary>
        private string transfomationFileNameRegexp = TransformationProvider.TranformationFilePattern;

        /// <summary>
        /// The source file regexp match index
        /// </summary>
        private int sourceFileRegexpMatchIndex = TransformationProvider.TranformationFileSourceMatchIndex;
        
        /// <summary>
        /// The nest transformation files
        /// </summary>
        private bool nestTransformationFiles = TransformationProvider.NestTransformationFiles;

        /// <summary>
        /// The write attributes on a separate line
        /// </summary>
        private bool writeAttributesOnASeparateLine = TransformationProvider.WriteAttributesOnASeparateLine;

        #endregion Fields

        #region Properties


        /// <summary>
        /// Gets or sets the transfomation file name regexp.
        /// </summary>
        /// <value>
        /// The transfomation file name regexp.
        /// </value>
        [Category("Basic Options")]
        // ReSharper disable once LocalizableElement
        [DisplayName("Transfomation file regexp")]
        [Description("Regexp to match transformation file name, e.g. '(.+)\\.(.+)\\.config' matches file 'App.Debug.config'")]
        public string TransfomationFileNameRegexp
        {
            get
            {
                return transfomationFileNameRegexp;
            }
            set
            {
                transfomationFileNameRegexp = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the index of the source file regexp match.
        /// </summary>
        /// <value>
        /// The index of the source file regexp match.
        /// </value>
        [Category("Basic Options")]
        // ReSharper disable once LocalizableElement
        [DisplayName("Source file regexp match index")]
        [Description("Index of the trans. file regexp to get source file name, e.g. for file 'App.Debug.config', regexp '(.+)\\.(.+)\\.config' and index 1 returns 'App'")]
        public int SourceFileRegexpMatchIndex
        {
            get
            {
                return sourceFileRegexpMatchIndex;
            }
            set
            {
                sourceFileRegexpMatchIndex = Math.Max(value, 0);
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [nest transformation files].
        /// </summary>
        /// <value>
        /// <c>true</c> if [nest transformation files]; otherwise, <c>false</c>.
        /// </value>
        [Category("Basic Options")]
        // ReSharper disable once LocalizableElement
        [DisplayName("Nest transformation files")]
        [Description("Option to set if transformation files should be nested under configuration file in solution explorer")]
        public bool NestTransformationFiles
        {
            get
            {
                return nestTransformationFiles;
            }
            set
            {
                nestTransformationFiles = value;
            }
        }

        [Category("Basic Options")]
        // ReSharper disable once LocalizableElement
        [DisplayName("Write attributes on a separate line")]
        [Description("Option to set if transformation file should have attribues on separate lines")]
        public bool WriteAttributesOnASeparateLine
        {
            get
            {
                return writeAttributesOnASeparateLine;
            }
            set
            {
                writeAttributesOnASeparateLine = value;
            }
        }

        #endregion Properties

        #region Event Handlers

        /// <summary>
        /// Handles "Close" messages from the Visual Studio environment.
        /// </summary>
        /// <devdoc>
        /// This event is raised when the page is closed.
        /// </devdoc>
        protected override void OnClosed(EventArgs e)
        {
            if (Options.NestTransformationFiles)
            {
                Logger.LogInfo(Resources.NestingFiles);
                FileNesting.ConfigFileNester.NestConfigurationFilesInSolution();
            }
            base.OnClosed(e);
        }


        ///// <summary>
        ///// Handles "Activate" messages from the Visual Studio environment.
        ///// </summary>
        ///// <devdoc>
        ///// This method is called when Visual Studio wants to activate this page.  
        ///// </devdoc>
        ///// <remarks>If the Cancel property of the event is set to true, the page is not activated.</remarks>
        //protected override void OnActivate(CancelEventArgs e)
        //{
        //    //DialogResult result = WinFormsHelper.ShowMessageBox("Press Cancel to cancel this deactivation.  OK to continue", "Press Cancel to cancel this deactivation.  OK to continue", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

        //    //if (result == DialogResult.Cancel)
        //    //{
        //    //    Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Cancelled the OnActivate event"));
        //    //    e.Cancel = true;
        //    //}

        //    base.OnActivate(e);
        //}



        ///// <summary>
        ///// Handles "Deactive" messages from the Visual Studio environment.
        ///// </summary>
        ///// <devdoc>
        ///// This method is called when VS wants to deactivate this
        ///// page.  If true is set for the Cancel property of the event, 
        ///// the page is not deactivated.
        ///// </devdoc>
        ///// <remarks>
        ///// A "Deactive" message is sent when a dialog page's user interface 
        ///// window loses focus or is minimized but is not closed.
        ///// </remarks>
        //protected override void OnDeactivate(CancelEventArgs e)
        //{
        //    //DialogResult result = WinFormsHelper.ShowMessageBox("Press Cancel to cancel this deactivation.  OK to continue", "Press Cancel to cancel this deactivation.  OK to continue", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

        //    //if (result == DialogResult.Cancel)
        //    //{
        //    //    Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Cancelled the OnDeactivate event"));
        //    //    e.Cancel = true;
        //    //}

        //    base.OnDeactivate(e);
        //}

        ///// <summary>
        ///// Handles Apply messages from the Visual Studio environment.
        ///// </summary>
        ///// <devdoc>
        ///// This method is called when VS wants to save the user's 
        ///// changes then the dialog is dismissed.
        ///// </devdoc>
        //protected override void OnApply(PageApplyEventArgs e)
        //{
        //    //DialogResult result = WinFormsHelper.ShowMessageBox("Press Cancel to cancel this OnApply.  OK to continue");

        //    //if (result == DialogResult.Cancel)
        //    //{
        //    //    Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Cancelled the OnApply event"));
        //    //    e.ApplyBehavior = ApplyKind.Cancel;
        //    //}
        //    //else
        //    //{
        //    //    base.OnApply(e);
        //    //}

        //    //WinFormsHelper.ShowMessageBox("In OnApply");

        //    base.OnApply(e);
        //}

        #endregion Event Handlers
    }
}
