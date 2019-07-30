<?php
namespace App\Controller;

use App\Controller\AppController;

/**
 * Steranking Controller
 *
 * @property \App\Model\Table\SterankingTable $Steranking
 *
 * @method \App\Model\Entity\Steranking[]|\Cake\Datasource\ResultSetInterface paginate($object = null, array $settings = [])
 */
class SterankingController extends AppController
{
    /**
     * Index method
     *
     * @return \Cake\Http\Response|null
     */
    public function index()
    {
        $steranking = $this->paginate($this->Steranking);

        $this->set(compact('steranking'));
    }

    /**
     * View method
     *
     * @param string|null $id Steranking id.
     * @return \Cake\Http\Response|null
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function view($id = null)
    {
        $steranking = $this->Steranking->get($id, [
            'contain' => []
        ]);

        $this->set('steranking', $steranking);
    }

    /**
     * Add method
     *
     * @return \Cake\Http\Response|null Redirects on successful add, renders view otherwise.
     */
    public function add()
    {
        $steranking = $this->Steranking->newEntity();
        if ($this->request->is('post')) {
            $steranking = $this->Steranking->patchEntity($steranking, $this->request->getData());
            if ($this->Steranking->save($steranking)) {
                $this->Flash->success(__('The steranking has been saved.'));

                return $this->redirect(['action' => 'index']);
            }
            $this->Flash->error(__('The steranking could not be saved. Please, try again.'));
        }
        $this->set(compact('steranking'));
    }

    /**
     * Edit method
     *
     * @param string|null $id Steranking id.
     * @return \Cake\Http\Response|null Redirects on successful edit, renders view otherwise.
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function edit($id = null)
    {
        $steranking = $this->Steranking->get($id, [
            'contain' => []
        ]);
        if ($this->request->is(['patch', 'post', 'put'])) {
            $steranking = $this->Steranking->patchEntity($steranking, $this->request->getData());
            if ($this->Steranking->save($steranking)) {
                $this->Flash->success(__('The steranking has been saved.'));

                return $this->redirect(['action' => 'index']);
            }
            $this->Flash->error(__('The steranking could not be saved. Please, try again.'));
        }
        $this->set(compact('steranking'));
    }

    /**
     * Delete method
     *
     * @param string|null $id Steranking id.
     * @return \Cake\Http\Response|null Redirects to index.
     * @throws \Cake\Datasource\Exception\RecordNotFoundException When record not found.
     */
    public function delete($id = null)
    {
        $this->request->allowMethod(['post', 'delete']);
        $steranking = $this->Steranking->get($id);
        if ($this->Steranking->delete($steranking)) {
            $this->Flash->success(__('The steranking has been deleted.'));
        } else {
            $this->Flash->error(__('The steranking could not be deleted. Please, try again.'));
        }

        return $this->redirect(['action' => 'index']);
    }

     public function getRanking()
	{
		$this->autoRender	= false;
		
		//テーブルからランキングリストをとってくる
        $query	= $this->Steranking->find("all");

        //クエリー処理を行う。
        $query->order(['Score'=>'DESC']);   //降順
        $query->limit(10);                  //取得件数を10件までに絞る
		
		//jsonにシリアライズする。
		$json	= json_encode($query);

		//jsonデータを返す。（レスポンスとして表示する。）
		echo $json;
    }

    public function setRanking()
    {
		$this->autoRender	= false;

        //POST パラメータを取得
        $postName   = $this->request->data("Name");
        $postScore  = $this->request->data("Score");

        $record = array(
            "Name"=>$postName,
            "Score"=>$postScore,
            "Date"=>date("Y/m/d H:i:s")
        );

        //テーブルにレコードを追加
        $prm1    = $this->Steranking->newEntity();
        $prm2    = $this->Steranking->patchEntity($prm1,$record);
        
        if( $this->Steranking->save($prm2) ){
            echo "Success";   //成功
        }else{
            echo "Error";   //失敗
        }
    }
}
