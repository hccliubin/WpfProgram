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

        static List<string> _movieTypes = new List<string>();
        /// <summary> 设置文件类型 </summary>
        public static List<string> MovieTypes
        {
            get
            {
                if (_movieTypes == null || _movieTypes.Count == 0)
                {
                    string str = Path.Combine(LocalCaseFolder, "DefaultFileType.ini");

                    if (!File.Exists(str))
                    {
                        return null;
                    }

                    string txt = File.ReadAllText(str, Encoding.Default);

                    _movieTypes = txt.Split(',').ToList();
                }


                return _movieTypes;
            }
            set
            {
                string str = Path.Combine(LocalCaseFolder, "DefaultFileType.ini");

                _movieTypes = value;

                if(value==null)
                {
                    File.WriteAllText(str, "");
                    return;
                }

                StringBuilder sb = new StringBuilder();

                foreach (var item in _movieTypes)
                {
                    sb.Append(item + ",");
                }

                File.WriteAllText(str,sb.ToString().Trim(','), Encoding.Default);
            }
        }

        static List<string> _matchTypes = new List<string>();
        /// <summary> 过滤文件类型 </summary>
        public static List<string> MatchTypes
        {
            get
            {
                if(_matchTypes==null|| _matchTypes.Count==0)
                {
                    string matchFile = Path.Combine(LocalCaseFolder, "MatchFileType.ini");

                    if (!File.Exists(matchFile)) return null;

                    string media = File.ReadAllText(matchFile, Encoding.Default);

                    var allextend = media.Split(new char[] { '、' }, StringSplitOptions.RemoveEmptyEntries);

                    if (allextend == null || allextend.Length == 0) return null;

                    _matchTypes = allextend.ToList();
                }

                return _matchTypes;
            }
            set
            {
                string matchFile = Path.Combine(LocalCaseFolder, "MatchFileType.ini");

                _matchTypes = value;

                if (value == null)
                {
                    File.WriteAllText(matchFile, "");
                    return;
                }

                StringBuilder sb = new StringBuilder();

                foreach (var item in _matchTypes)
                {
                    sb.Append(item + "、");
                }

                File.WriteAllText(matchFile, sb.ToString().Trim('、'), Encoding.Default);
            }
        }

        /// <summary> 加载所有案例名称 </summary>
        public List<string> LoadAllCaseName()
        {
            return Directory.GetFiles(LocalCaseFolder).ToList();
        }

        public const string extend = ".prj";

        /// <summary> 创建写入案例 </summary>
        public void CreateCase(CaseModel model)
        {
            if (model == null) return;

            string casePath = Path.Combine(model.FolderPath, model.CaseName);

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
                CaseModel model = this.LoadCase(item);

                if (model == null) continue;

                models.Add(model);
            }

            return models;
        }

        /// <summary> 加载案例 </summary>
        public CaseModel LoadCase(string folder)
        {
            var filePath = folder.GetAllFile(l => l.Extension == extend);

            if (filePath == null || filePath.Count == 0) return null;

            string json = File.ReadAllText(filePath[0]);

            CaseModel model = json.SerializeDeJson<CaseModel>();

            model.FolderPath = folder;

            return model;
        }

        /// <summary> 版本兼容 </summary>
        void VersionConvertSupport(CaseModel model)
        {

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
            if (model == null) return;

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

                foreach (var item in collection)
                {
                    this.VersionConvertSupport(item);
                }

                foreach (var item in _caseItems)
                {
                    item.Create(collection);
                }
            }
        }

        /// <summary> 版本兼容 </summary>
        void VersionConvertSupport(MovieFileModel item)
        {
            if (item.LastTime.Contains("Date"))
            {
                item.LastTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
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


                // Todo ：递归创建文件夹 
                int index = 0;

                Func<string, string> action = null;

                action = l =>
                      {
                          if (!Directory.Exists(l))
                          {
                              Directory.CreateDirectory(l);
                              return l;
                          }
                          else
                          {
                              index++;

                              string k = Path.Combine(model.CasePath, orderFoderName, fileNameWithOut + "（" + index + "）");

                              return action(k);

                          }
                      };

                string newfolder = action.Invoke(folder);


                //获取文件夹下所有名称相同的项和所有图片文件
                var allInfo = DirectoryHelper.GetAllFile(Path.GetDirectoryName(item), l => (Path.GetFileNameWithoutExtension(l.FullName) == fileNameWithOut && !l.FullName.Contains(orderFoderName)) || l.FullName.ToLower().EndsWith(",jpg") || l.FullName.ToLower().EndsWith(".bt"));



                foreach (var info in allInfo)
                {
                    string infoNewPath = Path.Combine(newfolder, Path.GetFileName(info));

                    File.Move(info, infoNewPath);
                }
            }

            this.RefreshLoad(model);

        }

        /// <summary> 重新加载 </summary>
        public void RefreshLoad(CaseModel model)
        {

            var collection = DirectoryHelper.GetAllFile(model.CasePath);

            var models = model.ListJson.SerializeDeJson<List<MovieFileModel>>();


            // Todo ：增加内存数据 
            foreach (var item in collection)
            {
                if (!this.MatchFile(item)) continue;

                var result = models.Find(l => l.FilePath == item);

                if (result != null) continue;

                MovieFileModel m = new MovieFileModel(item);
                models.Add(m);
            }


            // Todo ：删除无效数据 
            models.RemoveAll(l => !File.Exists(l.FilePath));

            model.ListJson = models.SerializeJson<List<MovieFileModel>>();

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

        /// <summary> 另存为 </summary>
        public void SaveOutLoad(CaseModel model, string path)
        {
            if (model == null) return;
            // Todo ：复制文件 

            //string newFolder = Path.Combine(path, model.CaseName);

            CaseModel caseModel = new CaseModel();

            caseModel.CopyFromObj(model);

            caseModel.FolderPath = path;

            this.CreateCase(caseModel);

            this.SaveCase(caseModel);

        }

        /// <summary> 另存为 </summary>
        public void MoveFolderLoad(CaseModel model, string path)
        {
            if (model == null) return;

            // Todo ：复制文件 
            var dirs = Directory.GetDirectories(model.CasePath);

            Dictionary<string, string> changeCatche = new Dictionary<string, string>();

            foreach (var item in dirs)
            {
                string newFolder = Path.GetFileNameWithoutExtension(item);

                newFolder = Path.Combine(path, newFolder);

                string indexFolder = newFolder.GetIndexFolderName();

                if (newFolder != indexFolder)
                {
                    // Todo ：记录要更换的名称 
                    changeCatche.Add(item, indexFolder);
                }

                Directory.Move(item, indexFolder);
            }

            // Todo ：修改内存

            var collection = model.ListJson.SerializeDeJson<List<MovieFileModel>>();

            foreach (var item in collection)
            {
                // Todo ：替换更名的 
                foreach (var c in changeCatche)
                {
                    item.FilePath = item.FilePath.Replace(c.Key, c.Value);
                }

                // Todo ：替换普通的 
                item.FilePath = item.FilePath.Replace(model.CasePath, path);
            }

            model.ListJson = collection.SerializeJson<List<MovieFileModel>>();

            model.CasePath = path;

            this.CreateCase(model);

        }

        /// <summary> 从指定案例合并 </summary>
        public void MergeCases(CaseModel model, List<CaseModel> froms)
        {
            List<MovieFileModel> cache = model.ListJson.SerializeDeJson<List<MovieFileModel>>();

            foreach (var item in froms)
            {
                // Todo ：复制文件 
                this.MoveFolderLoad(item, model.CasePath);

                // Todo ：修改内存 
                var models = item.ListJson.SerializeDeJson<List<MovieFileModel>>();

                cache.AddRange(models);
            }

            // Todo ：保存本地 
            string s = cache.SerializeJson<List<MovieFileModel>>();

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
