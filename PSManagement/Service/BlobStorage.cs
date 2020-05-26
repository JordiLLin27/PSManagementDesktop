using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

namespace PSManagement.Service
{
    /// <summary>
    /// Clase estática para guardar o eliminar imágenes en el servicio BlobStorage de Azure.
    /// </summary>
    public static class BlobStorage
    {
        private static readonly StorageCredentials credentials = new StorageCredentials(Properties.Settings.Default.AccountName, Properties.Settings.Default.KeyValue);
        private static readonly CloudStorageAccount storageacc = new CloudStorageAccount(credentials, true);
        private static readonly CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();
        private static readonly CloudBlobContainer container = blobClient.GetContainerReference(Properties.Settings.Default.ContainerName);

        /// <summary>
        /// Método para guardar una imágen en el servicio BlobStorage de Azure.
        /// </summary>
        /// <param name="filename">Nombre del archivo a guardar</param>
        /// <param name="blobReference">Nombre que hace referencia al archivo en el servicio</param>
        /// <returns>Ruta absoluta de la imágen guardada</returns>
        public static string GuardarImagen(string filename, string blobReference)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobReference);
            blockBlob.UploadFromFile(filename);

            return blockBlob.Uri.AbsoluteUri;
        }

        /// <summary>
        /// Elimina una imágen en el servicio BlobStorage de Azure
        /// </summary>
        /// <param name="blobReference">Nombre que hace referencia al archivo en el servicio</param>
        public static void EliminarImagen(string blobReference)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobReference);

            if (blockBlob.Exists())
            {
                blockBlob.Delete();
            }
        }
    }
}
