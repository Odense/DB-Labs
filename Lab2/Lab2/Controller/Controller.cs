using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.View;
using Lab2.Model;
using Lab2.Database;

namespace Lab2.Controller
{
    public enum Operation
    {
        GetById = 1,
        GetAll,
        Add,
        Update,
        Delete
    }
    class ControllerClass
    {

        private ViewClass view;
        private ClassDAO classDAO;
        private TeacherDAO teacherDAO;
        private PupilDAO pupilDAO;
        private LessonDAO lessonDAO;
        private JunctionDAO junctionDAO;
        private FullTextSearch fullTextSearch;

        public ControllerClass()
        {
            classDAO = new ClassDAO();
            teacherDAO = new TeacherDAO();
            pupilDAO = new PupilDAO();
            lessonDAO = new LessonDAO();
            junctionDAO = new JunctionDAO();
            fullTextSearch = new FullTextSearch();
            view = new ViewClass(classDAO.GetList(), lessonDAO.GetList());

            //DAO<Junction>.RandomDB(classDAO, teacherDAO, lessonDAO, pupilDAO, junctionDAO);
        }

        public void Start()
        {
            while (true)
            {
                int mode = view.MainMenu();
                if (mode == 0) break;
                else if (mode == 2)
                {
                    FullTextSearch();
                    view.Wait();
                }
                else if (mode == 3)
                {
                    SearchPupilOperation();
                    view.Wait();
                }
                else if (mode == 1)
                {
                    while (true)
                    {
                        Entity entity = view.EntitiesMenu();
                        if (entity == Entity.Null) break;
                        else if (entity != Entity.Exception)
                        {
                            if (entity == Entity.Junction)
                            {
                                GetListOperation();
                                view.Wait();
                            }
                            else while (true)
                                {
                                    int operation = view.OperationsMenu();
                                    if (operation == 0) break;
                                    try
                                    {
                                        switch ((Operation)operation)
                                        {
                                            case Operation.Add:
                                                AddOperation();
                                                break;
                                            case Operation.GetById:
                                                GetByIdOperation();
                                                break;
                                            case Operation.GetAll:
                                                GetListOperation();
                                                break;
                                            case Operation.Update:
                                                UpdateOperation();
                                                break;
                                            case Operation.Delete:
                                                DeleteOperation();
                                                break;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        view.Error(e.Message.ToString());
                                    }
                                    view.Wait();
                                }
                        }
                    }
                }
            }
        }

        private void AddOperation()
        {
            switch (view.entity)
            {
                case Entity.Pupil:
                    Pupil s = view.PupilAddOrUpdateEnter();
                    pupilDAO.Create(s);
                    break;
                case Entity.Teacher:
                    Teacher t = view.TeacherAddOrUpdateEnter();
                    teacherDAO.Create(t);
                    break;
            }
        }
        private void GetByIdOperation()
        {
            long id = -1;
            while (id < 0)
            {
                id = view.EnterId();
            }
            switch (view.entity)
            {
                case Entity.Pupil:
                    List<Pupil> pupils = new List<Pupil>() { pupilDAO.Get(id) };
                    view.PrintPupils(pupils);
                    break;
                case Entity.Teacher:
                    List<Teacher> teachers = new List<Teacher>() { teacherDAO.Get(id) };
                    view.PrintTeachers(teachers);
                    break;
            }
        }
        private void GetListOperation()
        {
            switch (view.entity)
                {
                    case Entity.Junction:
                        List<Junction> junctions = junctionDAO.GetList();
                        view.PrintJunctions(junctions);
                        break;
                    case Entity.Pupil:
                    List<Pupil> pupils = pupilDAO.GetList();
                        view.PrintPupils(pupils);
                        break;
                    case Entity.Teacher:
                        List<Teacher> teachers = teacherDAO.GetList();
                        view.PrintTeachers(teachers);
                        break;
            }
        }
        private void UpdateOperation()
        {
            long id = -1;
            while (id < 0)
            {
                id = view.EnterId();
            }
            if (id < 0) throw new Exception("Wrong id");
            switch (view.entity)
            {
                case Entity.Teacher:
                    Teacher m = view.TeacherAddOrUpdateEnter();
                    m.Id = id;
                    teacherDAO.Update(m);
                    break;
                case Entity.Pupil:
                    Pupil s = view.PupilAddOrUpdateEnter();
                    s.Id = id;
                    pupilDAO.Update(s);
                    break;
            }
        }
        private void DeleteOperation()
        {
            long id = -1;
            while (id < 0)
            {
                id = view.EnterId();
            }
            if (id < 0) throw new Exception("Wrong id");
            switch (view.entity)
            {
                case Entity.Teacher:
                    teacherDAO.Delete(id);
                    break;
                case Entity.Pupil:
                    pupilDAO.Delete(id);
                    break;
            }
        }

        private void SearchPupilOperation()
        {
            List<Pupil> t = pupilDAO.StaticSearch(view.StaticSearch());
            view.PrintPupils(t);
        }

        private void FullTextSearch()
        {
            List<SearchRes> res = new List<SearchRes>();
            string table = "pupils";
            string attr;
                int atr = view.PupilAtr();
            if (atr == 1) attr = "name";
            else attr = "surname";
            int search = view.FullText();
            string query = view.SearchQuery();
            switch (search)
            {
                case 1:
                    res.AddRange(fullTextSearch.getFullPhrase(attr, table, query));
                    view.PrintFullTextSearch_FullPhrase(res);
                    break;
                case 2:
                    res.AddRange(fullTextSearch.getAllWithNotIncludedWord(attr, table, query));
                    view.PrintFullTextSearch_NotIncludedWord(res);
                    break;
                default: throw new Exception("Wrong entity selected");
            }
        }
    }
}
