using System;
using System.Collections.Generic;

namespace ApplicationName.Core.Extensions
{
    public static class OperationResultExtensions
    {
        public static OperationResult<TResult> AddResult<TResult>(
            this OperationResult<TResult> operationResult,
            TResult result)
        {
            if (operationResult == null)
                throw new ArgumentNullException(nameof(operationResult), 
                    "builderContext cannot be null");
            operationResult.Result = result;
            return operationResult;
        }

        public static OperationResult<TResult> AddResultWithError<TResult>(
            this OperationResult<TResult> operationResult,
            TResult result,
            string errorMessage,
            int errorCode,
            Exception exception = null)
        {
            if (operationResult == null)
                throw new ArgumentNullException(nameof(operationResult),
                    "builderContext cannot be null"); 
            operationResult.Result = result;
            operationResult.Errors.Add(new ErrorResult(errorMessage, errorCode, exception));
            return operationResult;
        }

        public static OperationResult<TResult> AddResultWithError<TResult>(
            this OperationResult<TResult> operationResult,
            TResult result,
            ErrorResult errorResult)
        {
            if (operationResult == null)
                throw new ArgumentNullException(nameof(operationResult),
                    "builderContext cannot be null"); 
            operationResult.Result = result;
            operationResult.Errors.Add(errorResult);
            return operationResult;
        }

        public static OperationResult<TResult> AddError<TResult>(
            this OperationResult<TResult> operationResult,
            string errorMessage,
            int errorCode,
            Exception exception = null)
        {
            if (operationResult == null)
                throw new ArgumentNullException(nameof(operationResult),
                    "builderContext cannot be null");
            operationResult.Errors.Add(new ErrorResult(errorMessage, errorCode, exception));
            return operationResult;
        }

        public static OperationResult<TResult> AddError<TResult>(
            this OperationResult<TResult> operationResult,
            ErrorResult errorResult)
        {
            if (operationResult == null)
                throw new ArgumentNullException(nameof(operationResult),
                    "builderContext cannot be null");
            operationResult.Errors.Add(errorResult);
            return operationResult;
        }

        public static OperationResult<TResult> AddErrors<TResult>(
            this OperationResult<TResult> operationResult,
            IEnumerable<ErrorResult> validationErrors)
        {
            if (operationResult == null)
                throw new ArgumentNullException(nameof(operationResult),
                    "builderContext cannot be null");
            operationResult.Errors.AddRange(validationErrors);
            return operationResult;
        }

    }
}