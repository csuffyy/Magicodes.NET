﻿using Magicodes.Core.Web.Controllers.Viewer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

//======================================================================
//
//        Copyright (C) 2014-2016 Magicodes团队    
//        All rights reserved
//
//        filename :DocumentViewerController
//        description :
//
//        created by 雪雁 at  2014/12/18 14:50:17
//        http://www.magicodes.net
//
//======================================================================
namespace Magicodes.Core.Web.Controllers
{
    /// <summary>
    /// 寻找默认的文档加载器
    /// </summary>
    public class DocumentViewerController : PlusControllerBase
    {
        public ActionResult Index()
        {
            //文件路径
            var filePath = Request["filePath"];
            //文件协议
            var contentType = Request["contentType"];
            if (string.IsNullOrWhiteSpace(filePath))
                throw new HttpException("文件路径不能为空！");
            if (string.IsNullOrWhiteSpace(contentType))
                throw new HttpException("contentType：内容类型（MIME 类型）不能为空！");
            //获取协议
            var protocol = ApplicationContext.DocumentsOpenProtocolManager.DocumentOpenProtocols.FirstOrDefault(p => p.ContentType.Equals(contentType, StringComparison.CurrentCultureIgnoreCase));
            if (protocol != null)
            {
                //return RedirectToAction(protocol.ViewerUrl, new { FilePath = filePath });
                return View(protocol.ViewerUrl, new DocumentProtocolInfo() { FilePath = filePath, ContentType = contentType });
            }
            //如果不存在，则下载
            return File(filePath, contentType);
        }
    }
}
