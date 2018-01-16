using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EAP_zadania
{
    public class MatMulCompletedEventArgs : AsyncCompletedEventArgs
    {
        public double[,] matrix;
        public MatMulCompletedEventArgs(double[,] mat,Exception e, bool canceled, object state) : base(e, canceled, state)
        {
            matrix = mat;
        }

    }

    public class MatMulCalculator
    {
        public delegate void MatMulCompletedEventHandler(object sender, MatMulCompletedEventArgs e);
        private delegate void WorkerEventHandler(double[,] mat1, double[,] mat2,  AsyncOperation asyncOp);
        public event MatMulCompletedEventHandler MatMulCompleted;
        private SendOrPostCallback onCompletedCallback;
        private HybridDictionary userStateToLifetime = new HybridDictionary();

        public MatMulCalculator()
        {
            onCompletedCallback = new SendOrPostCallback(CalculateCompleted);
            
        }
        public void CalculateCompleted(object state)
        {
            MatMulCompletedEventArgs e = state as MatMulCompletedEventArgs;
            OnMatMulCalculatorCompleted(e);
            
        }

        protected void OnMatMulCalculatorCompleted(MatMulCompletedEventArgs e)
        {
            if (MatMulCompleted != null)
            {
                MatMulCompleted(this, e);
            }
        }

        void Completion(double[,] mat, Exception ex,bool cancelled, AsyncOperation ao)
        {
            if(!cancelled)
            {
                lock(userStateToLifetime.SyncRoot)
                {
                    userStateToLifetime.Remove(ao.UserSuppliedState);
                }
            }

            MatMulCompletedEventArgs e = new MatMulCompletedEventArgs(mat, ex, cancelled, ao.UserSuppliedState);

            ao.PostOperationCompleted(onCompletedCallback, e);

        }

        bool TaskCanceled(object taskId)
        {
            return (userStateToLifetime[taskId]==null);
        }

        void CalculateWorker(double[,] mat1, double[,] mat2,AsyncOperation ao)
        {
            Exception e = null;
            double[,] result = null;

            if (!TaskCanceled(ao.UserSuppliedState))
            {
                try
                {
                    result=MatMul(mat1, mat2);
                }
                catch (Exception ex)
                {
                    e = ex;           
                }
            }

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            this.Completion(result, e, TaskCanceled(ao.UserSuppliedState), ao);
        }

        double getVal(double[,] mat,int column,int row)
        {
            return mat[row, column];
        }

        double[,] MatMul (double[,] mat1, double[,] mat2)
        {
            if (mat1.Length!=mat2.Length)
            {
                throw new ArgumentException("Macierze musza byc tej samej dlugosci");
            }

            double[,] result = new double[mat1.GetLength(0), mat1.GetLength(1)];
            for (int i = 0; i < mat1.GetLength(0); i++)
            {
                for (int j = 0; j < mat1.GetLength(1); j++)
                {
                    for (int k = 0; k < mat1.GetLength(0); k++)
                    {
                        result[i, j] = result[i, k] * result[k, j];
                    }
                }
            }

            return result;
        }

        public virtual void MatMulAsync(double[,] mat1, double[,] mat2,object taskId)
        {
            AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(taskId);

            lock(userStateToLifetime.SyncRoot)
            {
                if (userStateToLifetime.Contains(taskId))
                {
                    throw new ArgumentException(
                        "Task ID parameter must be unique",
                        "taskId");
                }

                userStateToLifetime[taskId] = asyncOperation;
            }

            WorkerEventHandler workerDelegate = new WorkerEventHandler(CalculateWorker);
            workerDelegate.BeginInvoke(mat1, mat2, asyncOperation, null, null);

            
        }

        public void CancelAsync(object taskId)
        {
            AsyncOperation asyncOp = userStateToLifetime[taskId] as AsyncOperation;
            if (asyncOp != null)
            {
                lock (userStateToLifetime.SyncRoot)
                {
                    userStateToLifetime.Remove(taskId);
                }
            }
        }



    }
}
