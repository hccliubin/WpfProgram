#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/3/30 15:34:38 
 * 文件名：CaseNotifyService 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.Util;
using HeBianGu.General.ModuleManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.ModuleManager.Service
{
    public class CaseNotifyService
    {
        public static CaseNotifyService Instance = new CaseNotifyService();

        public event Action<CaseModel> CaseChanged;

        /// <summary> 触发切换案例事件 </summary> 
        public void OnCaseChanged(CaseModel model)
        {
            this.CreateCase(model);

            this.CreateCaseData(model);

            if (CaseChanged != null)
            {
                CaseChanged(model);
            }


        }

        public CaseNotifyService()
        {
            this.CaseItems.Add(new CommonItem());
            this.CaseItems.Add(new FavoriteItem());
            this.CaseItems.Add(new DeleteItem());
        }


        public event Action<CaseModel> BeforeSaveCase;

        public void OnSaveCase(CaseModel model)
        {
            if (BeforeSaveCase != null)
            {
                BeforeSaveCase(model);

                this.SaveCase(model);
            }
        }

        /// <summary> 获取案例文件全路径 </summary>
        public string LocalCasePath(string caseNamne)
        {
            return System.IO.Path.Combine(LocalCaseFolder, caseNamne);
        }


        /// <summary> 本地案例文件夹 </summary>
        public static string LocalCaseFolder
        {
            get
            {
                string p = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string d = System.IO.Path.Combine(p, "MovieBrowserToolApp");

                if (!Directory.Exists(d))
                {
                    Directory.CreateDirectory(d);
                }

                return d;
            }
        }


        /// <summary> 设置文件类型 </summary>
        public static List<string> MovieTypes
        {
            get
            {
                string str = Path.Combine(LocalCaseFolder, "DefaultFileType.ini");

                if (!File.Exists(str))
                {
                    return null;
                }

                string txt = File.ReadAllText(str, Encoding.Default);


                return txt.Split(',').ToList();
            }
        }


        /// <summary> 过滤文件类型 </summary>
        public static List<string> MatchTypes
        {
            get
            {
                string matchFile = Path.Combine(LocalCaseFolder, "MatchFileType.ini");

                if (!File.Exists(matchFile)) return null;

                string media = File.ReadAllText(matchFile, Encoding.Default);

                var allextend = media.Split(new char[] { '、' }, StringSplitOptions.RemoveEmptyEntries);

                if (allextend == null || allextend.Length == 0) return null;

                return allextend.ToList();
            }
        }



        /// <summary> 加载所有案例名称 </summary>
        public List<string> LoadAllCaseName()
        {
            return Directory.GetFiles(LocalCaseFolder).ToList();
        }

        const string extend = ".prj";

        const string extendFavorite = ".fav";


        /// <summary> 创建案例 </summary>
        public void CreateCase(CaseModel model)
        {
            string casePath = Path.Combine(LocalCaseFolder, model.CaseName);

            if (!Directory.Exists(casePath))
            {
                Directory.CreateDirectory(casePath);
            }

            string filePath = Path.Combine(casePath, model.CaseName + extend);

            string json = model.SerializeJson<CaseModel>();

            File.WriteAllText(filePath, json);
        }


        /// <summary> 加载所有案例 </summary>
        public List<CaseModel> LoadAllCase()
        {
            List<CaseModel> models = new List<CaseModel>();

            var collection = LocalCaseFolder.GetDirs();

            foreach (var item in collection)
            {
                var filePath = item.GetAllFile(l => l.Extension == extend);

                if (filePath == null || filePath.Count == 0) continue;

                string json = File.ReadAllText(filePath[0]);

                CaseModel model = json.SerializeDeJson<CaseModel>();

                models.Add(model);
            }

            return models;
        }

        public void DeleteCase(CaseModel mode)
        {
            Directory.Delete(this.LocalCasePath(mode.CaseName), true);
        }


        private List<ICaseItem> _caseItems = new List<ICaseItem>();
        /// <summary> 所有案例项缓存 </summary>
        public List<ICaseItem> CaseItems
        {
            get { return _caseItems; }
            set { _caseItems = value; }
        }

        /// <summary> 加载本例案例数据到内存 </summary>
        void CreateCaseData(CaseModel model)
        {
            // Todo ：加载列表 
            if (string.IsNullOrEmpty(model.ListJson))
            {
                var collection = DirectoryHelper.GetAllFile(model.CasePath, l => this.MatchFile(l.FullName));

                foreach (var item in this.CaseItems)
                {
                    item.Collection.Clear();
                }

                var normals = this.CaseItems.Find(l => l.FileType == FileType.Normal).Collection;

                foreach (var item in collection)
                {
                    normals.Add(new MovieFileModel(item));
                }
            }
            else
            {
                var collection = model.ListJson.SerializeDeJson<List<MovieFileModel>>();

                foreach (var item in _caseItems)
                {
                    item.Create(collection);
                }
            }
        }

        /// <summary> 整理文件 </summary>
        public void ClearOrder(CaseModel model)
        {
            string orderFoderName = "ACollection";

            var collection = DirectoryHelper.GetAllFile(model.CasePath);

            foreach (var item in collection)
            {
                if (!this.MatchFile(item)) continue;

                //// Todo ：规划好的格式不做处理 
                //if (Path.GetDirectoryName(Path.GetDirectoryName(item)) == model.CasePath) continue;

                if (item.Contains(orderFoderName)) continue;

                string fileName = Path.GetFileName(item);

                string fileNameWithOut = Path.GetFileNameWithoutExtension(item);

                string folder = Path.Combine(model.CasePath, orderFoderName, fileNameWithOut);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // Todo ：复制文件 
                //string newPath = Path.Combine(folder, fileName);

                //if (File.Exists(newPath))
                //{
                //    File.Delete(newPath);
                //}

                //获取文件夹下所有名称相同的项
                var allInfo = DirectoryHelper.GetAllFile(Path.GetDirectoryName(item), l => Path.GetFileNameWithoutExtension(l.FullName) == fileNameWithOut);

                foreach (var info in allInfo)
                {
                    string infoName = Path.GetFileName(info);
                    string infoNewPath = Path.Combine(folder, infoName);

                    if (File.Exists(infoNewPath))
                    {
                        File.Delete(infoNewPath);
                    }

                    File.Move(info, infoNewPath);
                }
                //File.Move(item, newPath);
            }


            // Todo ：清空数据 
            model.ListJson = null;

            this.CreateCase(model);

        }

        public bool MatchFile(string fileFullName)
        {
            if (MatchTypes == null) return true;

            string extend = Path.GetExtension(fileFullName).Trim('.').ToLower();

            return MatchTypes.Contains(extend);
        }

        /// <summary> 保存案例数据到本地 </summary>
        public void SaveCase(CaseModel model)
        {
            if (model == null) return;
            List<MovieFileModel> models = new List<MovieFileModel>();

            foreach (var item in this.CaseItems)
            {
                models.AddRange(item.Collection);
            }

            string s = models.SerializeJson<List<MovieFileModel>>();


            model.ListJson = s;

            this.CreateCase(model);
        }

    }


    public interface ICaseItem
    {
        List<MovieFileModel> Collection { get; set; }

        FileType FileType { get; set; }

        void Create(List<MovieFileModel> collection);

    }

    public abstract class CaseItem : ICaseItem
    {
        private List<MovieFileModel> _collection = new List<MovieFileModel>();
        /// <summary> 说明 </summary>
        public List<MovieFileModel> Collection
        {
            get { return _collection; }
            set { _collection = value; }
        }


        private FileType fileType;
        /// <summary> 说明 </summary>
        public FileType FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }

        public void Create(List<MovieFileModel> collection)
        {
            this.Collection = collection.FindAll(l => l.FileType == fileType);
        }


    }

    public class CommonItem : CaseItem
    {
        public CommonItem()
        {
            this.FileType = FileType.Normal;
        }
    }

    public class FavoriteItem : CaseItem
    {
        public FavoriteItem()
        {
            this.FileType = FileType.Favorate;
        }
    }

    public class DeleteItem : CaseItem
    {
        public DeleteItem()
        {
            this.FileType = FileType.Delete;
        }
    }
}
